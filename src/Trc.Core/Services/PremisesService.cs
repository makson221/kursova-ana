using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Облік приміщень ТРЦ.</summary>
public class PremisesService : ServiceBase
{
    public PremisesService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<Premises> GetAll(string? search = null, PremisesStatus? status = null)
    {
        using var db = ContextFactory();
        var query = db.Premises.AsNoTracking();
        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        var list = query.OrderBy(p => p.Floor).ThenBy(p => p.Number).ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(p => ContainsText(p.Number, search) || ContainsText(p.Description, search)
                                   || ContainsText(p.Type.GetDisplayName(), search)).ToList();
        }
        return list;
    }

    /// <summary>Приміщення, які можна передати в оренду.</summary>
    public List<Premises> GetAvailable() => GetAll(status: PremisesStatus.Free);

    public Premises Get(int id)
    {
        using var db = ContextFactory();
        return db.Premises.AsNoTracking().FirstOrDefault(p => p.Id == id)
               ?? throw new BusinessException("Приміщення не знайдено.");
    }

    public int Add(Premises premises)
    {
        Validate(premises);
        using var db = ContextFactory();
        EnsureUniqueNumber(db, premises.Number, premises.Id);
        if (premises.Status == PremisesStatus.Occupied)
            throw new BusinessException("Статус «Орендоване» встановлюється автоматично під час укладання договору.");

        var entity = new Premises();
        CopyFields(premises, entity);
        entity.Status = premises.Status;
        db.Premises.Add(entity);
        db.SaveChanges();
        return entity.Id;
    }

    public void Update(Premises premises)
    {
        Validate(premises);
        using var db = ContextFactory();
        var entity = db.Premises.Find(premises.Id) ?? throw new BusinessException("Приміщення не знайдено.");
        EnsureUniqueNumber(db, premises.Number, premises.Id);

        var hasActiveLease = db.Leases.Any(l => l.PremisesId == premises.Id && l.Status == LeaseStatus.Active);
        if (hasActiveLease && premises.Status != PremisesStatus.Occupied)
            throw new BusinessException("Приміщення має діючий договір оренди — змінити його статус неможливо.");
        if (!hasActiveLease && premises.Status == PremisesStatus.Occupied)
            throw new BusinessException("Статус «Орендоване» встановлюється автоматично під час укладання договору.");

        CopyFields(premises, entity);
        entity.Status = premises.Status;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = ContextFactory();
        var entity = db.Premises.Find(id) ?? throw new BusinessException("Приміщення не знайдено.");
        if (db.Leases.Any(l => l.PremisesId == id))
            throw new BusinessException("Приміщення має історію договорів оренди, тому його не можна видалити.");

        db.Premises.Remove(entity);
        db.SaveChanges();
    }

    private static void CopyFields(Premises source, Premises target)
    {
        target.Number = source.Number.Trim().ToUpperInvariant();
        target.Floor = source.Floor;
        target.Area = source.Area;
        target.Type = source.Type;
        target.RatePerSqm = source.RatePerSqm;
        target.Description = string.IsNullOrWhiteSpace(source.Description) ? null : source.Description.Trim();
    }

    private static void Validate(Premises p)
    {
        Guard.Required(p.Number, "Номер приміщення");
        Guard.Positive(p.Area, "Площа");
        Guard.NotNegative(p.RatePerSqm, "Ставка оренди");
        if (p.Floor is < -3 or > 10)
            throw new BusinessException("Поверх має бути в межах від -3 до 10.");
    }

    private static void EnsureUniqueNumber(TrcDbContext db, string number, int id)
    {
        var normalized = number.Trim().ToUpperInvariant();
        if (db.Premises.Any(p => p.Number == normalized && p.Id != id))
            throw new BusinessException($"Приміщення з номером «{normalized}» вже існує.");
    }
}
