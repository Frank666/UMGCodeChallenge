using Microsoft.AspNetCore.Mvc;
using UMGCodeChallenge.Application.endpoints;
using UMGCodeChallenge.Domain;
using UMGCodeChallenge.Infrastructure;

namespace UMGCodeChallenge.endpoints;

public class ProcessFile
{
    private readonly IContractServiceFactory _serviceFactory;

    public ProcessFile(IContractServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public IResult UploadContracts([FromForm] ContractUploadRequest request)
    {
        if (request.MusicContracts == null || request.PartnerContracts == null)
            return Results.BadRequest("Both files are required");

        if (string.IsNullOrWhiteSpace(request.Partner))
            return Results.BadRequest("Partner name is required");

        if (string.IsNullOrWhiteSpace(request.Date))
            return Results.BadRequest("Date name is required");

        if (!DateTime.TryParse(request.Date, out var effectiveDate))
            return Results.BadRequest("Date format is not valid please use yyyy-MM-dd");

        try
        {
            var musicContracts = LoadMusicContracts(request.MusicContracts);
            var partnerContracts = LoadPartnerContracts(request.PartnerContracts);

            var service = _serviceFactory.Create(musicContracts, partnerContracts);
            var results = service.GetActiveContracts(request.Partner, effectiveDate);

            return Results.Ok(results);
        }
        catch (InvalidDataException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Internal server error: {ex.Message}");
        }
    }

    private IEnumerable<MusicContract> LoadMusicContracts(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var lines = reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return FileDataLoader.LoadMusicContracts(lines);
    }

    private IEnumerable<PartnerContract> LoadPartnerContracts(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var lines = reader.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return FileDataLoader.LoadPartnerContracts(lines);
    }
}

