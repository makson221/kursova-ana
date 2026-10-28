using Trc.Core.Common;
using Trc.Core.Models;

namespace Trc.Core.Data;

/// <summary>Створення бази даних та заповнення її демонстраційними даними.</summary>
public static class DbInitializer
{
    public const string DefaultAdminLogin = "admin";
    public const string DefaultAdminPassword = "admin123";

    /// <summary>
    /// Створює схему БД (якщо її ще немає). Якщо база порожня — додає користувачів
    /// за замовчуванням, а за потреби й демонстраційні дані.
    /// </summary>
    public static void Initialize(TrcDbContext db, IClock clock, bool seedDemoData)
    {
        db.Database.EnsureCreated();
        if (db.Users.Any())
            return;

        AddUser(db, DefaultAdminLogin, "Іваненко Олена Петрівна", UserRole.Administrator, DefaultAdminPassword);
        AddUser(db, "manager", "Петренко Сергій Іванович", UserRole.Manager, "manager123");
        AddUser(db, "cashier", "Сидоренко Марія Олегівна", UserRole.Cashier, "cashier123");
        db.SaveChanges();

        if (seedDemoData)
            SeedDemoData(db, clock.Now);
    }

    private static void AddUser(TrcDbContext db, string login, string fullName, UserRole role, string password)
    {
        var (hash, salt) = PasswordHasher.HashPassword(password);
        db.Users.Add(new User { Login = login, FullName = fullName, Role = role, PasswordHash = hash, PasswordSalt = salt });
    }

    private static void SeedDemoData(TrcDbContext db, DateTime now)
    {
        var today = now.Date;
        var random = new Random(2026);

        // --- Приміщення ---
        Premises P(string number, int floor, decimal area, PremisesType type, decimal rate, string? description) =>
            new() { Number = number, Floor = floor, Area = area, Type = type, RatePerSqm = rate, Description = description };

        var premises = new List<Premises>
        {
            P("Б-001", -1, 200, PremisesType.Storage, 150, "Складське приміщення, вантажний ліфт"),
            P("1-001", 1, 120, PremisesType.Shop, 650, "Біля центрального входу"),
            P("1-002", 1, 85, PremisesType.Shop, 650, "Вітрина на атріум"),
            P("1-003", 1, 420, PremisesType.Shop, 480, "Під супермаркет, окремий вхід для постачання"),
            P("1-004", 1, 40, PremisesType.Service, 700, "Острівне розміщення"),
            P("1-005", 1, 95, PremisesType.Shop, 650, "Кутове приміщення"),
            P("2-001", 2, 150, PremisesType.Shop, 550, null),
            P("2-002", 2, 70, PremisesType.Shop, 550, null),
            P("2-003", 2, 110, PremisesType.Shop, 550, "Поруч з ескалатором"),
            P("2-004", 2, 60, PremisesType.Service, 600, "Підведена вода, вентиляція"),
            P("2-005", 2, 130, PremisesType.Shop, 550, null),
            P("3-001", 3, 35, PremisesType.FoodCourt, 800, "Фуд-корт, місце №1"),
            P("3-002", 3, 30, PremisesType.FoodCourt, 800, "Фуд-корт, місце №2"),
            P("3-003", 3, 180, PremisesType.Restaurant, 500, "Літня тераса"),
            P("3-004", 3, 600, PremisesType.Entertainment, 300, "Дитячий розважальний центр"),
            P("3-005", 3, 900, PremisesType.Cinema, 250, "4 кінозали")
        };
        db.Premises.AddRange(premises);

        // --- Орендарі ---
        Tenant T(string name, string code, TenantCategory category, string contact, string phone, string? email) =>
            new() { CompanyName = name, TaxCode = code, Category = category, ContactPerson = contact, Phone = phone, Email = email };

        var tenants = new List<Tenant>
        {
            T("ТОВ «Модний квартал»", "41234567", TenantCategory.Clothing, "Литвиненко Оксана", "+380671234501", "office@modkvartal.ua"),
            T("ТОВ «Техно Плюс»", "38765432", TenantCategory.Electronics, "Бондар Олег", "+380502345602", "rent@technoplus.ua"),
            T("ТОВ «Свіжий кошик»", "40112233", TenantCategory.Grocery, "Шевчук Наталія", "+380673456703", "info@koshyk.ua"),
            T("ФОП Коваль Ірина Василівна", "3012345678", TenantCategory.Beauty, "Коваль Ірина", "+380934567804", null),
            T("ТОВ «Смачна піца»", "42001122", TenantCategory.Food, "Ткаченко Дмитро", "+380665678905", "pizza@smachna.ua"),
            T("ТОВ «Кава та десерт»", "43556677", TenantCategory.Food, "Кравець Юлія", "+380976789006", "hello@kava-desert.ua"),
            T("ТОВ «Фан Парк»", "39887766", TenantCategory.Entertainment, "Олійник Максим", "+380637890107", "park@funpark.ua"),
            T("ТОВ «Кіно Сіті»", "37445566", TenantCategory.Entertainment, "Мороз Віктор", "+380448901208", "admin@kinocity.ua"),
            T("ТОВ «Взуттєвий світ»", "41998877", TenantCategory.Clothing, "Савченко Тетяна", "+380679012309", "shoes@vsvit.ua"),
            T("ФОП Мельник Андрій Олегович", "2987654321", TenantCategory.Services, "Мельник Андрій", "+380951234510", "melnyk.service@gmail.com")
        };
        db.Tenants.AddRange(tenants);
        db.SaveChanges();

        // --- Договори оренди ---
        var firstOfMonth = new DateTime(today.Year, today.Month, 1);
        var leasePlan = new (int Tenant, string Premises, int MonthsAgo, int Years)[]
        {
            (0, "1-001", 14, 3), (1, "1-002", 9, 2), (2, "1-003", 20, 5), (3, "2-004", 7, 1),
            (4, "3-001", 11, 2), (5, "3-002", 5, 1), (6, "3-004", 18, 3), (7, "3-005", 24, 5),
            (8, "2-001", 6, 2), (9, "1-004", 4, 1)
        };

        var leases = new List<Lease>();
        var sequence = 1;
        foreach (var (tenantIndex, number, monthsAgo, years) in leasePlan)
        {
            var place = premises.Single(p => p.Number == number);
            var start = firstOfMonth.AddMonths(-monthsAgo);
            var lease = new Lease
            {
                Number = $"Д-{start.Year}-{sequence++:000}",
                Tenant = tenants[tenantIndex],
                Premises = place,
                StartDate = start,
                EndDate = start.AddYears(years).AddDays(-1),
                MonthlyRent = place.BaseMonthlyRent,
                Deposit = place.BaseMonthlyRent,
                Status = LeaseStatus.Active
            };
            place.Status = PremisesStatus.Occupied;
            leases.Add(lease);
        }

        // Один завершений договір для історії.
        var oldStart = firstOfMonth.AddMonths(-30);
        leases.Add(new Lease
        {
            Number = $"Д-{oldStart.Year}-{sequence:000}",
            Tenant = tenants[0],
            Premises = premises.Single(p => p.Number == "2-005"),
            StartDate = oldStart,
            EndDate = oldStart.AddYears(2).AddDays(-1),
            MonthlyRent = 60000,
            Deposit = 60000,
            Status = LeaseStatus.Expired
        });
        db.Leases.AddRange(leases);
        premises.Single(p => p.Number == "Б-001").Status = PremisesStatus.Maintenance;

        // --- Нарахування та оплати за останні 6 місяців ---
        var debtorTenants = new HashSet<int> { 4, 9 }; // «Смачна піца» та ФОП Мельник мають борги
        foreach (var lease in leases.Where(l => l.Status == LeaseStatus.Active))
        {
            for (var i = 5; i >= 0; i--)
            {
                var period = firstOfMonth.AddMonths(-i);
                if (period < lease.StartDate)
                    continue;

                var payment = new Payment
                {
                    Lease = lease,
                    PeriodYear = period.Year,
                    PeriodMonth = period.Month,
                    Amount = lease.MonthlyRent,
                    DueDate = period.AddDays(9)
                };

                var tenantIndex = tenants.IndexOf(lease.Tenant);
                var isDebt = debtorTenants.Contains(tenantIndex) && i <= 1;
                if (i > 0 && !isDebt || i == 0 && random.Next(3) == 0 && !debtorTenants.Contains(tenantIndex))
                {
                    payment.Status = PaymentStatus.Paid;
                    var paid = payment.DueDate.AddDays(-random.Next(0, 8));
                    payment.PaidDate = paid > today ? today : paid;
                }
                db.Payments.Add(payment);
            }
        }

        // --- Працівники ---
        Employee E(string name, string position, Department dep, decimal salary, string phone, int monthsAgo) =>
            new() { FullName = name, Position = position, Department = dep, Salary = salary, Phone = phone, HireDate = today.AddMonths(-monthsAgo) };

        db.Employees.AddRange(
            E("Гончаренко Василь Миколайович", "Директор ТРЦ", Department.Administration, 65000, "+380671110001", 60),
            E("Руденко Алла Сергіївна", "Головний бухгалтер", Department.Administration, 42000, "+380671110002", 48),
            E("Петренко Сергій Іванович", "Менеджер з оренди", Department.Administration, 32000, "+380671110003", 30),
            E("Захарченко Ігор Петрович", "Начальник служби безпеки", Department.Security, 35000, "+380671110004", 40),
            E("Лисенко Роман Васильович", "Охоронець", Department.Security, 18000, "+380671110005", 20),
            E("Кузьменко Артем Ігорович", "Охоронець", Department.Security, 18000, "+380671110006", 12),
            E("Демченко Галина Іванівна", "Прибиральниця", Department.Cleaning, 14000, "+380671110007", 36),
            E("Поліщук Світлана Олексіївна", "Прибиральниця", Department.Cleaning, 14000, "+380671110008", 8),
            E("Марченко Олександр Юрійович", "Головний інженер", Department.Technical, 38000, "+380671110009", 50),
            E("Тарасенко Віталій Андрійович", "Електрик", Department.Technical, 22000, "+380671110010", 15),
            E("Вовк Анастасія Дмитрівна", "Маркетолог", Department.Marketing, 30000, "+380671110011", 10),
            E("Сидоренко Марія Олегівна", "Касир", Department.Parking, 17000, "+380671110012", 6),
            E("Білик Павло Степанович", "Оператор паркінгу", Department.Parking, 16500, "+380671110013", 3));

        // --- Розважальні заходи ---
        var events = new List<MallEvent>
        {
            new() { Title = "Новорічний дитячий концерт", Location = "Атріум, 1 поверх", StartTime = today.AddDays(-20).AddHours(17),
                    Capacity = 150, TicketPrice = 150, Status = EventStatus.Completed, Description = "Святкова програма для дітей" },
            new() { Title = "Майстер-клас з кулінарії", Location = "Фуд-корт, 3 поверх", StartTime = today.AddDays(3).AddHours(12),
                    Capacity = 30, TicketPrice = 350, Description = "Готуємо піцу разом з шеф-кухарем" },
            new() { Title = "Stand-up вечір", Location = "Кінозал №1", StartTime = today.AddDays(7).AddHours(19),
                    Capacity = 120, TicketPrice = 400 },
            new() { Title = "Фестиваль настільних ігор", Location = "Розважальна зона, 3 поверх", StartTime = today.AddDays(14).AddHours(11),
                    Capacity = 80, TicketPrice = 100, Description = "Турніри, призи, новинки ігор" },
            new() { Title = "Модний показ осінньої колекції", Location = "Атріум, 1 поверх", StartTime = today.AddDays(21).AddHours(18),
                    Capacity = 200, TicketPrice = 0, Description = "Вхід вільний за реєстрацією" }
        };
        db.Events.AddRange(events);

        string[] buyers = { "Гнатюк О.", "Ярошенко В.", "Лук'яненко С.", "Остапенко Н.", "Карпенко Д.", "Сергієнко І." };
        foreach (var ev in events)
        {
            var count = ev.Status == EventStatus.Completed ? 12 : random.Next(2, 7);
            for (var i = 0; i < count; i++)
            {
                var quantity = random.Next(1, 4);
                ev.Tickets.Add(new Ticket
                {
                    BuyerName = buyers[random.Next(buyers.Length)],
                    Quantity = quantity,
                    TotalPrice = quantity * ev.TicketPrice,
                    SoldAt = Min(ev.StartTime.AddDays(-random.Next(1, 10)).Date.AddHours(random.Next(10, 21)), now),
                    SoldBy = "cashier"
                });
            }
        }

        // --- Паркінг: історія за 30 днів і авто, що зараз на стоянці ---
        var parking = new ParkingSettings();
        string[] regions = { "AA", "KA", "BC", "AI", "AX", "BH", "KI", "AE" };
        string RandomPlate() =>
            $"{regions[random.Next(regions.Length)]}{random.Next(1000, 9999)}{(char)('A' + random.Next(26))}{(char)('A' + random.Next(26))}";

        for (var day = 30; day >= 1; day--)
        {
            for (var i = 0; i < 6; i++)
            {
                var entry = today.AddDays(-day).AddHours(random.Next(9, 20)).AddMinutes(random.Next(60));
                var exit = entry.AddMinutes(random.Next(10, 300));
                db.ParkingSessions.Add(new ParkingSession
                {
                    PlateNumber = RandomPlate(),
                    EntryTime = entry,
                    ExitTime = exit,
                    Fee = Services.ParkingService.CalculateFee(entry, exit, parking)
                });
            }
        }

        for (var i = 0; i < 7; i++)
        {
            db.ParkingSessions.Add(new ParkingSession { PlateNumber = RandomPlate(), EntryTime = now.AddMinutes(-20 - 35 * i) });
        }

        db.SaveChanges();
    }

    private static DateTime Min(DateTime a, DateTime b) => a < b ? a : b;
}
