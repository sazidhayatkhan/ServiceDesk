namespace ServiceDesk.Api.Modules.Locations;

public class LocationService
{
    private readonly LocationRepository _repository;

    public LocationService(LocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Location> CreateAsync(CreateLocationRequest request)
    {
        var existing = await _repository.GetByCodeAsync(request.Code);

        if (existing is not null)
        {
            throw new Exception("Location code already exists.");
        }

        var location = new Location
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Code = request.Code,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(location);
        await _repository.SaveChangesAsync();

        return location;
    }

    public Task<List<Location>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }
}