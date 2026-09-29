using Microsoft.EntityFrameworkCore;
using ServiceDesk.Api.Modules.Locations;
using ServiceDesk.Api.Modules.Requests;
using ServiceDesk.Api.Modules.Users;

namespace ServiceDesk.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Location> Locations => Set<Location>();

    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
}