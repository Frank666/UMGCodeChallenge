using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Application.endpoints;
public class ContractServiceFactory : IContractServiceFactory
{
    public ContractService Create(IEnumerable<MusicContract> musicContracts, IEnumerable<PartnerContract> partnerContracts)
    {
        return new ContractService(musicContracts, partnerContracts);
    }
}