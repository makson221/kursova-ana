using Trc.Core.Common;
using Trc.Core.Models;

namespace Trc.Tests;

public class EventServiceTests
{
    private static int AddEvent(TestDatabase t, int capacity = 10, decimal price = 100) =>
        t.Events.Add(new MallEvent
        {
            Title = "Концерт", Location = "Атріум", Capacity = capacity, TicketPrice = price,
            StartTime = t.Clock.Now.AddDays(5)
        });

    [Fact]
    public void SellTickets_CalculatesTotalPrice()
    {
        using var t = new TestDatabase();
        var id = AddEvent(t, price: 250);
        var ticket = t.Events.SellTickets(id, "Покупець", 3);
        Assert.Equal(750m, ticket.TotalPrice);
        Assert.Equal(3, t.Events.GetSoldCount(id));
    }

    [Fact]
    public void SellTickets_CannotExceedCapacity()
    {
        using var t = new TestDatabase();
        var id = AddEvent(t, capacity: 5);
        t.Events.SellTickets(id, "Перший", 4);
        var ex = Assert.Throws<BusinessException>(() => t.Events.SellTickets(id, "Другий", 2));
        Assert.Contains("1", ex.Message);
    }

    [Fact]
    public void SellTickets_ClosedAfterStart()
    {
        using var t = new TestDatabase();
        var id = AddEvent(t);
        t.Clock.Advance(TimeSpan.FromDays(6));
        Assert.Throws<BusinessException>(() => t.Events.SellTickets(id, "Пізній", 1));
    }

    [Fact]
    public void Cancel_RefundsAllTickets()
    {
        using var t = new TestDatabase();
        var id = AddEvent(t);
        t.Events.SellTickets(id, "А", 2);
        t.Events.SellTickets(id, "Б", 3);

        Assert.Equal(5, t.Events.Cancel(id));
        Assert.Equal(0, t.Events.GetSoldCount(id));
        Assert.Equal(EventStatus.Cancelled, t.Events.Get(id).Status);
    }

    [Fact]
    public void Update_CannotReduceCapacityBelowSold()
    {
        using var t = new TestDatabase();
        var id = AddEvent(t, capacity: 10);
        t.Events.SellTickets(id, "А", 6);
        var ev = t.Events.Get(id);
        ev.Capacity = 5;
        Assert.Throws<BusinessException>(() => t.Events.Update(ev));
    }
}
