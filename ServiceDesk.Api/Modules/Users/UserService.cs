namespace ServiceDesk.Api.Modules.Users;

public class UserService
{
    private readonly UserRepository _userRepository;

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

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}