using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Application;

public class ContractService : IContractService
{
    private readonly IEnumerable<MusicContract> _musicContracts;
    private readonly IEnumerable<PartnerContract> _partnerContracts;

    public ContractService(IEnumerable<MusicContract> musicContracts, IEnumerable<PartnerContract> partnerContracts)
    {
        _musicContracts = musicContracts;
        _partnerContracts = partnerContracts;
    }

    public IEnumerable<MusicContract> GetActiveContracts(string partnerName, DateTime date)
    {
        var partnerUsages = _partnerContracts
            .Where(p => p.Partner.Equals(partnerName, StringComparison.OrdinalIgnoreCase))
            .Select(p => p.Usage)
            .ToList();

        if (!partnerUsages.Any())
            return Enumerable.Empty<MusicContract>();

        return _musicContracts.Where(mc => mc.IsActive(date) && mc.Usages.Any(u => partnerUsages.Contains(u)));
    }
}