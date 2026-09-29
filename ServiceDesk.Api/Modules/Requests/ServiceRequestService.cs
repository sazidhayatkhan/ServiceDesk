using ServiceDesk.Api.Modules.Locations;

namespace ServiceDesk.Api.Modules.Requests;

public class ServiceRequestService
{
    private readonly ServiceRequestRepository _repository;
    private readonly LocationRepository _locationRepository;

    private static ServiceRequestResponse ToResponse(
    ServiceRequest request)
    {
        return new ServiceRequestResponse
        {
            Id = request.Id,
            LocationId = request.LocationId,
            AssignedUserId = request.AssignedUserId,
            Description = request.Description,
            Status = request.Status,
            CreatedAt = request.CreatedAt,
            CompletedAt = request.CompletedAt
        };
    }

    public ServiceRequestService(
        ServiceRequestRepository repository,
        LocationRepository locationRepository)
    {
        _repository = repository;
        _locationRepository = locationRepository;
    }

    public async Task<ServiceRequestResponse> CreateAsync(
        CreateServiceRequest request)
    {
        var location =
            await _locationRepository.GetByIdAsync(request.LocationId);

        if (location is null)
        {
            throw new Exception("Location not found.");
        }

        var serviceRequest = new ServiceRequest
        {
            Id = Guid.NewGuid(),
            LocationId = request.LocationId,
            Description = request.Description,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(serviceRequest);
        await _repository.SaveChangesAsync();

        return ToResponse(serviceRequest);
    }

    public async Task<List<ServiceRequestResponse>> GetAllAsync()
    {
        var requests = await _repository.GetAllAsync();

        return requests
            .Select(ToResponse)
            .ToList();
    }

    public async Task<ServiceRequestResponse> UpdateStatusAsync(
    Guid id,
    string status)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request is null)
        {
            throw new KeyNotFoundException(
        "Service request not found.");
        }

        var allowedStatuses = new[]
        {
        "Pending",
        "InProgress",
        "Completed"
    };

        if (!allowedStatuses.Contains(status))
        {
            throw new ArgumentException(
       "Invalid status.");
        }

        request.Status = status;

        if (status == "Completed")
        {
            request.CompletedAt = DateTime.UtcNow;
        }

        await _repository.SaveChangesAsync();

        return ToResponse(request);
    }
}