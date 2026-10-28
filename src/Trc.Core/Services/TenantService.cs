using Microsoft.EntityFrameworkCore;
using Trc.Core.Common;
using Trc.Core.Data;
using Trc.Core.Models;

namespace Trc.Core.Services;

/// <summary>Облік орендарів.</summary>
public class TenantService : ServiceBase
{
    public TenantService(Func<TrcDbContext> contextFactory, IClock clock) : base(contextFactory, clock) { }

    public List<Tenant> GetAll(string? search = null)
    {
        using var db = ContextFactory();
        var list = db.Tenants.AsNoTracking()
            .Include(t => t.Leases)
            .OrderBy(t => t.CompanyName)
            .ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            list = list.Where(t => ContainsText(t.CompanyName, search) || ContainsText(t.TaxCode, search)
                                   || ContainsText(t.ContactPerson, search) || ContainsText(t.Phone, search)
                                   || ContainsText(t.Category.GetDisplayName(), search)).ToList();
        }
        return list;
    }

    public Tenant Get(int id)
    {
        using var db = ContextFactory();
        return db.Tenants.AsNoTracking().FirstOrDefault(t => t.Id == id)
               ?? throw new BusinessException("Орендаря не знайдено.");
    }

    public int Add(Tenant tenant)
    {
        Validate(tenant);
        using var db = ContextFactory();
        EnsureUniqueTaxCode(db, tenant.TaxCode, tenant.Id);

        var entity = new Tenant();
        CopyFields(tenant, entity);
        db.Tenants.Add(entity);
        db.SaveChanges();
        return entity.Id;
    }

    public void Update(Tenant tenant)
    {
        Validate(tenant);
        using var db = ContextFactory();
        var entity = db.Tenants.Find(tenant.Id) ?? throw new BusinessException("Орендаря не знайдено.");
        EnsureUniqueTaxCode(db, tenant.TaxCode, tenant.Id);
        CopyFields(tenant, entity);
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = ContextFactory();
        var entity = db.Tenants.Find(id) ?? throw new BusinessException("Орендаря не знайдено.");
        if (db.Leases.Any(l => l.TenantId == id))
            throw new BusinessException("Орендар має договори оренди, тому його не можна видалити.");

        db.Tenants.Remove(entity);
        db.SaveChanges();
    }

    private static void CopyFields(Tenant source, Tenant target)
    {
        target.CompanyName = source.CompanyName.Trim();
        target.TaxCode = source.TaxCode.Trim();
        target.Category = source.Category;
        target.ContactPerson = source.ContactPerson.Trim();
        target.Phone = source.Phone.Trim();
        target.Email = string.IsNullOrWhiteSpace(source.Email) ? null : source.Email.Trim();
    }

    private static void Validate(Tenant t)
    {
        Guard.Required(t.CompanyName, "Назва орендаря");
        var code = Guard.Required(t.TaxCode, "Код ЄДРПОУ / РНОКПП");
        Guard.Matches(code, Patterns.TaxCode(), "Код ЄДРПОУ має містити 8 цифр, а РНОКПП (для ФОП) — 10 цифр.");
        Guard.Required(t.ContactPerson, "Контактна особа");
        var phone = Guard.Required(t.Phone, "Телефон");
        Guard.Matches(phone, Patterns.Phone(), "Невірний формат телефону. Приклад: +380671234567.");
        if (!string.IsNullOrWhiteSpace(t.Email))
            Guard.Matches(t.Email.Trim(), Patterns.Email(), "Невірний формат електронної пошти.");
    }

    private static void EnsureUniqueTaxCode(TrcDbContext db, string code, int id)
    {
        code = code.Trim();
        if (db.Tenants.Any(t => t.TaxCode == code && t.Id != id))
            throw new BusinessException($"Орендар з кодом {code} вже зареєстрований.");
    }
}
