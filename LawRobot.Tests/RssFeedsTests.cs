using LawRobot.Core.Feeds;
using Microsoft.Extensions.Configuration;

namespace LawRobot.Tests;

public class RssFeedsTests
{
    [Theory]
    [InlineData("appsettings.json")]
    [InlineData("appsettings.Development.json")]
    public void AppSettings_RssFeeds_MatchCanonicalCoreConstants(string configFile)
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "TestConfig", configFile);

        var config = new ConfigurationBuilder()
            .AddJsonFile(configPath)
            .Build();

        Assert.Equal(RssFeeds.Senate, config["RssFeeds:Senate"]);
        Assert.Equal(RssFeeds.House, config["RssFeeds:House"]);
    }
}
