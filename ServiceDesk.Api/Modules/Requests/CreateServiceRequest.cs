using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Api.Modules.Requests;

public class CreateServiceRequest
{
    [Required]
    public Guid LocationId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
}