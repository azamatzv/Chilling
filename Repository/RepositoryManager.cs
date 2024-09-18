using Contracts;
using Entities;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private IUserRepository _userRepository;

    public RepositoryManager(RepositoryContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public IUserRepository User
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }
            return _userRepository;
        }
    }

    public Task SaveAsync() => _context.SaveChangesAsync();
}