namespace Trc.Core.Common;

/// <summary>
/// Порушення бізнес-правила або некоректні вхідні дані.
/// Повідомлення призначене для показу користувачу.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
