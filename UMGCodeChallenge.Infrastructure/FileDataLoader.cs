using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Infrastructure;

public class FileDataLoader
{
    public static IEnumerable<MusicContract> LoadMusicContracts(IEnumerable<string> lines)
    {
        int lineNumber = 1;
        foreach (var line in lines.Skip(1)) // skip header
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length < 5)
                throw new InvalidDataException($"Error on line: {lineNumber}: expecting five columns, columns found it: {parts.Length}");

            var artist = parts[0].Trim();
            var title = parts[1].Trim();
            var usagesRaw = parts[2].Split(',');
            var startDateRaw = parts[3].Trim();
            var endDateRaw = parts[4].Trim();

            if (!DateTime.TryParse(startDateRaw, out var startDate))
                throw new InvalidDataException($"Error on line {lineNumber}: invalid StartDate ({startDateRaw})");

            DateTime? endDate = null;
            if (!string.IsNullOrWhiteSpace(endDateRaw))
            {
                if (!DateTime.TryParse(endDateRaw, out var parsedEnd))
                    throw new InvalidDataException($"Error on line {lineNumber}: invalid EndDate ({endDateRaw})");
                endDate = parsedEnd;
            }

            var usages = usagesRaw
                .Select(u => UsageTypeMapper.Parse(u, lineNumber))
                .ToList();

            yield return new MusicContract(artist, title, usages, startDate, endDate);
        }
    }

    public static IEnumerable<PartnerContract> LoadPartnerContracts(IEnumerable<string> lines)
    {
        int lineNumber = 1;
        foreach (var line in lines.Skip(1))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length < 2)
                throw new InvalidDataException($"Error on line {lineNumber}: expecting two columns, columns found it: {parts.Length}");

            var partner = parts[0].Trim();
            var usage = UsageTypeMapper.Parse(parts[1], lineNumber);

            yield return new PartnerContract(partner, usage);
        }
    }
}