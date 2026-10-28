using Trc.Core.Common;
using Trc.Core.Models;
using Trc.Core.Services;

namespace Trc.Tests;

public class ParkingServiceTests
{
    private static readonly ParkingSettings Settings = new() { HourlyRate = 20, FreeMinutes = 15, DailyMaximum = 200 };
    private static readonly DateTime Entry = new(2026, 3, 15, 10, 0, 0);

    [Theory]
    [InlineData(10, 0)]        // безкоштовні 15 хвилин
    [InlineData(15, 0)]
    [InlineData(16, 20)]       // розпочата година
    [InlineData(60, 20)]
    [InlineData(61, 40)]
    [InlineData(150, 60)]
    [InlineData(600, 200)]     // 10 год, але не більше добового максимуму
    [InlineData(1500, 220)]    // 1 доба + 1 година
    public void CalculateFee_FollowsTariff(int minutes, decimal expected)
    {
        Assert.Equal(expected, ParkingService.CalculateFee(Entry, Entry.AddMinutes(minutes), Settings));
    }

    [Theory]
    [InlineData("aa 1234 bb", "AA1234BB")]
    [InlineData("КА-0001-ХО", "KA0001XO")]   // кирилиця
    public void NormalizePlate_Works(string input, string expected)
    {
        Assert.Equal(expected, ParkingService.NormalizePlate(input));
    }

    [Fact]
    public void RegisterEntry_RejectsInvalidPlate()
    {
        using var t = new TestDatabase();
        Assert.Throws<BusinessException>(() => t.Parking.RegisterEntry("123"));
    }

    [Fact]
    public void RegisterEntry_RejectsDuplicateAndRespectsCapacity()
    {
        using var t = new TestDatabase();
        t.Parking.RegisterEntry("AA1111AA");
        Assert.Throws<BusinessException>(() => t.Parking.RegisterEntry("aa 1111 aa"));

        t.Parking.RegisterEntry("BB2222BB");
        var ex = Assert.Throws<BusinessException>(() => t.Parking.RegisterEntry("CC3333CC"));
        Assert.Contains("Вільних місць", ex.Message);
    }

    [Fact]
    public void RegisterExit_CalculatesFee()
    {
        using var t = new TestDatabase();
        var session = t.Parking.RegisterEntry("AA1234BB");
        t.Clock.Advance(TimeSpan.FromMinutes(95));

        var closed = t.Parking.RegisterExit(session.Id);
        Assert.Equal(40m, closed.Fee);
        Assert.Empty(t.Parking.GetActive());
        Assert.Throws<BusinessException>(() => t.Parking.RegisterExit(session.Id));
    }
}
