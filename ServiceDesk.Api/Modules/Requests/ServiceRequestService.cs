using ServiceDesk.Api.Modules.Locations;

namespace ServiceDesk.Api.Modules.Requests;

public class ServiceRequestService
{
    private readonly ServiceRequestRepository _repository;
    private readonly LocationRepository _locationRepository;

    public ServiceRequestService(
        ServiceRequestRepository repository,
        LocationRepository locationRepository)
    {
        _repository = repository;
        _locationRepository = locationRepository;
    }

    public async Task<ServiceRequest> CreateAsync(
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

        return serviceRequest;
    }

    public Task<List<ServiceRequest>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public async Task<ServiceRequest> UpdateStatusAsync(
    Guid id,
    string status)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request is null)
        {
            throw new Exception("Service request not found.");
        }

        var allowedStatuses = new[]
        {
        "Pending",
        "InProgress",
        "Completed"
    };

        if (!allowedStatuses.Contains(status))
        {
            throw new Exception("Invalid status.");
        }

        request.Status = status;

        if (status == "Completed")
        {
            request.CompletedAt = DateTime.UtcNow;
        }

        await _repository.SaveChangesAsync();

        return request;
    }
}