using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Api.Modules.Locations;

public class CreateLocationRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
}