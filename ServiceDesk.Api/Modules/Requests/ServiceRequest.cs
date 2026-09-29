namespace ServiceDesk.Api.Modules.Requests;

public class ServiceRequest
{
    public Guid Id { get; set; }

    public Guid LocationId { get; set; }

    public Guid? AssignedUserId { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
}