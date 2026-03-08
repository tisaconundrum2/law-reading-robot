using LawRobot.Core.Services;
using Microsoft.Extensions.Configuration;

namespace LawRobot.Tests;

public class ContentHashServiceTests
{
    private static HmacSha256ContentHashService CreateService(string secret = "test-secret")
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ContentHash:HmacSecret"] = secret
            })
            .Build();

        return new HmacSha256ContentHashService(config);
    }

    [Fact]
    public void ComputeHash_ReturnsDeterministicLowercaseHex()
    {
        var service = CreateService();

        var hash1 = service.ComputeHash("hello world");
        var hash2 = service.ComputeHash("hello world");

        Assert.Equal(hash1, hash2);
        Assert.Equal(64, hash1.Length);
        Assert.Matches("^[0-9a-f]{64}$", hash1);
    }

    [Fact]
    public void HasChanged_ReturnsFalse_ForMatchingHashDifferentCase()
    {
        var service = CreateService();
        var hash = service.ComputeHash("same text").ToUpperInvariant();

        Assert.False(service.HasChanged("same text", hash));
    }

    [Fact]
    public void HasChanged_ReturnsTrue_WhenTextDiffers()
    {
        var service = CreateService();
        var storedHash = service.ComputeHash("old");

        Assert.True(service.HasChanged("new", storedHash));
    }

    [Fact]
    public void Constructor_ThrowsWhenMissingSecret()
    {
        var config = new ConfigurationBuilder().Build();

        Assert.Throws<InvalidOperationException>(() => new HmacSha256ContentHashService(config));
    }
}
