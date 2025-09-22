namespace UMGCodeChallenge.Domain;

public record MusicContract(string Artist, string Title, IEnumerable<UsageType> Usages, DateTime StartDate, DateTime? EndDate)
{
    public bool IsActive(DateTime date) => StartDate <= date && (EndDate == null || EndDate >= date);
}