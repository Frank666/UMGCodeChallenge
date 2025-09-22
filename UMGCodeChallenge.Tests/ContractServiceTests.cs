using UMGCodeChallenge.Application;
using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Tests;

public class ContractServiceTests
{
    private readonly List<MusicContract> _musicContracts;
    private readonly List<PartnerContract> _partnerContracts;

    public ContractServiceTests()
    {
        _musicContracts = new List<MusicContract>
        {
            new MusicContract("Monkey Claw","Motor Mouth", new[]{ UsageType.DigitalDownload, UsageType.Streaming }, new DateTime(2011,3,1), null),
            new MusicContract("Tinie Tempah","Frisky", new[]{ UsageType.DigitalDownload }, new DateTime(2012,2,1), null),
            new MusicContract("Monkey Claw","Christmas Special", new[]{ UsageType.Streaming }, new DateTime(2012,12,25), new DateTime(2012,12,31))
        };

        _partnerContracts = new List<PartnerContract>
        {
            new PartnerContract("ITunes", UsageType.DigitalDownload),
            new PartnerContract("YouTube", UsageType.Streaming)
        };
    }

    [Fact]
    public void GetActiveContracts_ITunes_2012_03_01_ShouldReturnCorrectContracts()
    {
        // Arrange
        var service = new ContractService(_musicContracts, _partnerContracts);
        var partner = "ITunes";
        var date = new DateTime(2012, 3, 1);

        // Act
        var result = service.GetActiveContracts(partner, date).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Title == "Motor Mouth");
        Assert.Contains(result, c => c.Title == "Frisky");
    }

    [Fact]
    public void GetActiveContracts_YouTube_2012_12_27_ShouldReturnCorrectContracts()
    {
        // Arrange
        var service = new ContractService(_musicContracts, _partnerContracts);
        var partner = "YouTube";
        var date = new DateTime(2012, 12, 27);

        // Act
        var result = service.GetActiveContracts(partner, date).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Title == "Motor Mouth");
        Assert.Contains(result, c => c.Title == "Christmas Special");
    }

    [Fact]
    public void GetActiveContracts_NoMatchingPartner_ShouldReturnEmpty()
    {
        // Arrange
        var service = new ContractService(_musicContracts, _partnerContracts);
        var partner = "Spotify";
        var date = new DateTime(2012, 3, 1);

        // Act
        var result = service.GetActiveContracts(partner, date);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetActiveContracts_DateBeforeAllContracts_ShouldReturnEmpty()
    {
        // Arrange
        var service = new ContractService(_musicContracts, _partnerContracts);
        var partner = "ITunes";
        var date = new DateTime(2010, 1, 1);

        // Act
        var result = service.GetActiveContracts(partner, date);

        // Assert
        Assert.Empty(result);
    }
}