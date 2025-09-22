using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Application;

public interface IContractService
{
    IEnumerable<MusicContract> GetActiveContracts(string partner, DateTime date);
}
