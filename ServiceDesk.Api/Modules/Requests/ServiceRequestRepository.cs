using Microsoft.EntityFrameworkCore;
using ServiceDesk.Api.Data;

namespace ServiceDesk.Api.Modules.Requests;

public class ServiceRequestRepository
{
    private readonly AppDbContext _db;

    public ServiceRequestRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ServiceRequest>> GetAllAsync()
    {
        return await _db.ServiceRequests
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ServiceRequest?> GetByIdAsync(Guid id)
    {
        return await _db.ServiceRequests
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ServiceRequest request)
    {
        await _db.ServiceRequests.AddAsync(request);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}