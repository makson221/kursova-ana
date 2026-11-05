using System.Security.Cryptography;

namespace Trc.Core.Common;

/// <summary>
/// Хешування паролів алгоритмом PBKDF2 (SHA-256) з випадковою «сіллю».
/// Паролі у відкритому вигляді в базі даних не зберігаються.
/// Алгоритм реалізовано за RFC 8018 на основі HMAC-SHA256, тому він не залежить
/// від системної реалізації PBKDF2 (якої, наприклад, немає у Wine).
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public static (string Hash, string Salt) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Pbkdf2Sha256(password, salt, Iterations, HashSize);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public static bool Verify(string password, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var expected = Convert.FromBase64String(hash);
        var actual = Pbkdf2Sha256(password, saltBytes, Iterations, HashSize);
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }

    /// <summary>PBKDF2 з HMAC-SHA256 (RFC 8018, розділ 5.2).</summary>
    internal static byte[] Pbkdf2Sha256(string password, byte[] salt, int iterations, int length)
    {
        using var hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(password));
        var result = new byte[length];
        var block = new byte[salt.Length + 4];
        Buffer.BlockCopy(salt, 0, block, 0, salt.Length);

        for (int blockIndex = 1, offset = 0; offset < length; blockIndex++)
        {
            // U1 = HMAC(P, S || INT(i))
            block[^4] = (byte)(blockIndex >> 24);
            block[^3] = (byte)(blockIndex >> 16);
            block[^2] = (byte)(blockIndex >> 8);
            block[^1] = (byte)blockIndex;
            var u = hmac.ComputeHash(block);
            var t = (byte[])u.Clone();

            // T = U1 xor U2 xor ... xor Uc
            for (var i = 1; i < iterations; i++)
            {
                u = hmac.ComputeHash(u);
                for (var j = 0; j < t.Length; j++)
                    t[j] ^= u[j];
            }

            var count = Math.Min(t.Length, length - offset);
            Buffer.BlockCopy(t, 0, result, offset, count);
            offset += count;
        }
        return result;
    }
}
