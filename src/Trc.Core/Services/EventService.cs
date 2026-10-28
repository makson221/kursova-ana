using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Захід разом із кількістю проданих квитків.</summary>
public sealed record EventInfo(MallEvent Event, int SoldTickets)
{
    public int FreeSeats => Math.Max(0, Event.Capacity - SoldTickets);
}

/// <summary>Розважальні заходи та продаж квитків.</summary>
public class EventService : ServiceBase
{
    public EventService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<EventInfo> GetAll(bool includePast = true, string? search = null)
    {
        using var db = ContextFactory();
        var now = Clock.Now;
        var query = db.Events.AsNoTracking();
        if (!includePast)
            query = query.Where(e => e.StartTime >= now);

        var list = query
            .OrderBy(e => e.StartTime)
            .Select(e => new { Event = e, Sold = e.Tickets.Sum(t => (int?)t.Quantity) ?? 0 })
            .ToList()
            .Select(x => new EventInfo(x.Event, x.Sold))
            .ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(x => ContainsText(x.Event.Title, search) || ContainsText(x.Event.Location, search)).ToList();
        }
        return list;
    }

    public MallEvent Get(int id)
    {
        using var db = ContextFactory();
        return db.Events.AsNoTracking().FirstOrDefault(e => e.Id == id)
               ?? throw new BusinessException("Захід не знайдено.");
    }

    public int GetSoldCount(int eventId)
    {
        using var db = ContextFactory();
        return SoldCount(db, eventId);
    }

    public int Add(MallEvent mallEvent)
    {
        Validate(mallEvent);
        if (mallEvent.StartTime <= Clock.Now)
            throw new BusinessException("Неможливо запланувати захід на минулу дату.");

        using var db = ContextFactory();
        var entity = new MallEvent { Status = EventStatus.Planned };
        CopyFields(mallEvent, entity);
        db.Events.Add(entity);
        db.SaveChanges();
        return entity.Id;
    }

    public void Update(MallEvent mallEvent)
    {
        Validate(mallEvent);
        using var db = ContextFactory();
        var entity = db.Events.Find(mallEvent.Id) ?? throw new BusinessException("Захід не знайдено.");
        if (entity.Status == EventStatus.Cancelled)
            throw new BusinessException("Скасований захід редагувати не можна.");

        var sold = SoldCount(db, entity.Id);
        if (mallEvent.Capacity < sold)
            throw new BusinessException($"Уже продано {sold} квитків — кількість місць не може бути меншою.");
        if (sold > 0 && mallEvent.TicketPrice != entity.TicketPrice)
            throw new BusinessException("Ціну квитка не можна змінити після початку продажу.");

        CopyFields(mallEvent, entity);
        db.SaveChanges();
    }

    /// <summary>Скасування заходу. Продані квитки повертаються (видаляються).</summary>
    public int Cancel(int id)
    {
        using var db = ContextFactory();
        var entity = db.Events.Include(e => e.Tickets).FirstOrDefault(e => e.Id == id)
                     ?? throw new BusinessException("Захід не знайдено.");
        if (entity.Status != EventStatus.Planned)
            throw new BusinessException("Скасувати можна лише запланований захід.");

        var refunded = entity.Tickets.Sum(t => t.Quantity);
        db.Tickets.RemoveRange(entity.Tickets);
        entity.Status = EventStatus.Cancelled;
        db.SaveChanges();
        return refunded;
    }

    public void Delete(int id)
    {
        using var db = ContextFactory();
        var entity = db.Events.Find(id) ?? throw new BusinessException("Захід не знайдено.");
        if (db.Tickets.Any(t => t.MallEventId == id))
            throw new BusinessException("На захід продано квитки. Спочатку скасуйте його.");

        db.Events.Remove(entity);
        db.SaveChanges();
    }

    /// <summary>Продаж квитків з перевіркою наявності вільних місць.</summary>
    public Ticket SellTickets(int eventId, string buyerName, int quantity, string? soldBy = null)
    {
        buyerName = Guard.Required(buyerName, "Покупець");
        if (quantity < 1)
            throw new BusinessException("Кількість квитків має бути не меншою за 1.");
        if (quantity > 20)
            throw new BusinessException("За одну операцію можна продати не більше 20 квитків.");

        using var db = ContextFactory();
        var entity = db.Events.Find(eventId) ?? throw new BusinessException("Захід не знайдено.");
        if (entity.Status != EventStatus.Planned)
            throw new BusinessException("Продаж квитків на цей захід закрито.");
        if (entity.StartTime <= Clock.Now)
            throw new BusinessException("Захід уже розпочався — продаж квитків неможливий.");

        var free = entity.Capacity - SoldCount(db, eventId);
        if (quantity > free)
            throw new BusinessException(free == 0 ? "Усі квитки продано." : $"Залишилося лише {free} вільних місць.");

        var ticket = new Ticket
        {
            MallEventId = eventId,
            BuyerName = buyerName,
            Quantity = quantity,
            TotalPrice = entity.TicketPrice * quantity,
            SoldAt = Clock.Now,
            SoldBy = soldBy
        };
        db.Tickets.Add(ticket);
        db.SaveChanges();
        return ticket;
    }

    public List<Ticket> GetTickets(int eventId)
    {
        using var db = ContextFactory();
        return db.Tickets.AsNoTracking().Where(t => t.MallEventId == eventId).OrderByDescending(t => t.SoldAt).ToList();
    }

    /// <summary>Повернення квитків до початку заходу.</summary>
    public void RefundTicket(int ticketId)
    {
        using var db = ContextFactory();
        var ticket = db.Tickets.Include(t => t.Event).FirstOrDefault(t => t.Id == ticketId)
                     ?? throw new BusinessException("Продаж не знайдено.");
        if (ticket.Event.StartTime <= Clock.Now)
            throw new BusinessException("Захід уже відбувся — повернення неможливе.");

        db.Tickets.Remove(ticket);
        db.SaveChanges();
    }

    /// <summary>Позначає минулі заплановані заходи як такі, що відбулися.</summary>
    public int CompletePastEvents()
    {
        using var db = ContextFactory();
        var now = Clock.Now;
        var past = db.Events.Where(e => e.Status == EventStatus.Planned && e.StartTime < now).ToList();
        past.ForEach(e => e.Status = EventStatus.Completed);
        db.SaveChanges();
        return past.Count;
    }

    private static int SoldCount(TrcDbContext db, int eventId) =>
        db.Tickets.Where(t => t.MallEventId == eventId).Sum(t => (int?)t.Quantity) ?? 0;

    private static void Validate(MallEvent e)
    {
        Guard.Required(e.Title, "Назва заходу");
        Guard.Required(e.Location, "Місце проведення");
        if (e.Capacity < 1)
            throw new BusinessException("Кількість місць має бути не меншою за 1.");
        Guard.NotNegative(e.TicketPrice, "Ціна квитка");
    }

    private static void CopyFields(MallEvent source, MallEvent target)
    {
        target.Title = source.Title.Trim();
        target.Description = string.IsNullOrWhiteSpace(source.Description) ? null : source.Description.Trim();
        target.StartTime = source.StartTime;
        target.Location = source.Location.Trim();
        target.Capacity = source.Capacity;
        target.TicketPrice = source.TicketPrice;
    }
}
