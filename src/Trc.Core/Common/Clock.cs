namespace Trc.Core.Common;

/// <summary>Джерело поточного часу (абстракція потрібна для модульних тестів).</summary>
public interface IClock
{
    DateTime Now { get; }
    DateTime Today => Now.Date;
}

public sealed class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}

/// <summary>Годинник із фіксованим часом, який можна змінювати вручну.</summary>
public sealed class FixedClock : IClock
{
    public FixedClock(DateTime now) => Now = now;

    public DateTime Now { get; set; }

    public void Advance(TimeSpan delta) => Now += delta;
}
