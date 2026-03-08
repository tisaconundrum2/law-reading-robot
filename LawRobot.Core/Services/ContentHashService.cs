using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LawRobot.Core.Services;

public interface IContentHashService
{
    string ComputeHash(string text);
    bool HasChanged(string text, string storedHash);
}

public class HmacSha256ContentHashService : IContentHashService
{
    private readonly byte[] _key;

    public HmacSha256ContentHashService(IConfiguration config)
    {
        var secret = config["ContentHash:HmacSecret"]
            ?? throw new InvalidOperationException("ContentHash:HmacSecret not configured");
        _key = Encoding.UTF8.GetBytes(secret);
    }

    public string ComputeHash(string text)
    {
        using var hmac = new HMACSHA256(_key);
        var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    public bool HasChanged(string text, string storedHash)
        => !string.Equals(ComputeHash(text), storedHash, StringComparison.OrdinalIgnoreCase);
}
