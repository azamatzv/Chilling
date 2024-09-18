using Entities.Model.User;

namespace Contracts;

public interface IUserRepository
{
    Task<User> LoginAsync(string login, string password, bool tracking, CancellationToken cancellationToken = default);
}