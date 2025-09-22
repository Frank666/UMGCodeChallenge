using UMGCodeChallenge.Domain;
using UMGCodeChallenge.Infrastructure;

namespace UMGCodeChallenge.Tests;

public class FileDataLoaderTests
{
    [Fact]
    public void LoadMusicContracts_ValidData_ShouldParseCorrectly()
    {
        // Arrange
        var lines = new List<string>
        {
            "Artist|Title|Usages|StartDate|EndDate",
            "Monkey Claw|Motor Mouth|digital download, streaming|2011-03-01|",
            "Tinie Tempah|Frisky|digital download|2012-02-01|"
        };

        // Act
        var contracts = FileDataLoader.LoadMusicContracts(lines).ToList();

        // Assert
        Assert.Equal(2, contracts.Count);
        Assert.Contains(contracts, c => c.Artist == "Monkey Claw" && c.Usages.Contains(UsageType.Streaming));
        Assert.Contains(contracts, c => c.Artist == "Tinie Tempah" && c.Usages.Contains(UsageType.DigitalDownload));
    }

    [Fact]
    public void LoadMusicContracts_InvalidUsage_ShouldThrow()
    {
        // Arrange
        var lines = new List<string>
        {
            "Artist|Title|Usages|StartDate|EndDate",
            "Artist1|Song1|invalid usage|2022-01-01|"
        };

        // Act & Assert
        var ex = Assert.Throws<InvalidDataException>(() => FileDataLoader.LoadMusicContracts(lines).ToList());
        Assert.Contains("invalid Usage", ex.Message);
    }

    [Fact]
    public void LoadPartnerContracts_ValidData_ShouldParseCorrectly()
    {
        // Arrange
        var lines = new List<string>
        {
            "Partner|Usage",
            "ITunes|digital download",
            "YouTube|streaming"
        };

        // Act
        var partners = FileDataLoader.LoadPartnerContracts(lines).ToList();

        // Assert
        Assert.Equal(2, partners.Count);
        Assert.Contains(partners, p => p.Partner == "ITunes" && p.Usage == UsageType.DigitalDownload);
        Assert.Contains(partners, p => p.Partner == "YouTube" && p.Usage == UsageType.Streaming);
    }
}