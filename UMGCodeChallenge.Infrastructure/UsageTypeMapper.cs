using UMGCodeChallenge.Domain;

namespace UMGCodeChallenge.Infrastructure;

public static class UsageTypeMapper
{
    public static UsageType Parse(string raw, int lineNumber)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new InvalidDataException($"Error on line {lineNumber}: empty Usage");

        return raw.Trim().ToLowerInvariant() switch
        {
            "digital download" => UsageType.DigitalDownload,
            "streaming" => UsageType.Streaming,
            _ => throw new InvalidDataException($"Error on line {lineNumber}: invalid Usage ({raw})")
        };
    }
}