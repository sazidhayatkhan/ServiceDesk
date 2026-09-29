namespace ServiceDesk.Api.Modules.Users;

using Microsoft.AspNetCore.Identity;
public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateAsync(CreateUserRequest request)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            throw new Exception("Email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.Password,
            Role = "Staff",
            CreatedAt = DateTime.UtcNow
        };
        
        user.PasswordHash =
                  _passwordHasher.HashPassword(
                      user,
                      request.Password
                  );

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}