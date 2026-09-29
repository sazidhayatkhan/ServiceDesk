namespace ServiceDesk.Api.Modules.Users;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateAsync(User user)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(user.Email);

        if (existingUser is not null)
        {
            throw new Exception("Email already exists.");
        }

        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}