using Contracts;
using Entities;
using Entities.Model.User;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
        
    }

    public async Task<User> LoginAsync(string login, string password, bool tracking, CancellationToken cancellationToken = default)
        => await FindByCondition(x => x.Login.Equals(login) && x.Password.Equals(password), tracking).SingleOrDefaultAsync(cancellationToken);


}