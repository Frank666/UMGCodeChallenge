using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Application.endpoints;
public interface IContractServiceFactory
{
    ContractService Create(IEnumerable<MusicContract> musicContracts, IEnumerable<PartnerContract> partnerContracts);
}