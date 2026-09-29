using Microsoft.EntityFrameworkCore;
using ServiceDesk.Api.Data;

namespace ServiceDesk.Api.Modules.Locations;

public class LocationRepository
{
    private readonly AppDbContext _db;

    public LocationRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Location>> GetAllAsync()
    {
        return await _db.Locations
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(Guid id)
    {
        return await _db.Locations
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Location?> GetByCodeAsync(string code)
    {
        return await _db.Locations
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task AddAsync(Location location)
    {
        await _db.Locations.AddAsync(location);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}