namespace ServiceDesk.Api.Modules.Requests;

public class ServiceRequestResponse
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid? AssignedUserId { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}