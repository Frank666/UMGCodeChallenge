using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UMGCodeChallenge.Domain;

public class ContractUploadRequest
{
    [Required]
    public IFormFile MusicContracts { get; set; } = default!;

    [Required]
    public IFormFile PartnerContracts { get; set; } = default!;

    [Required]
    public string Partner { get; set; } = string.Empty;

    [Required]
    public string Date { get; set; } = string.Empty;
}
