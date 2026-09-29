using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Api.Modules.Requests;

public class UpdateRequestStatus
{
    [Required]
    public string Status { get; set; } = string.Empty;
}