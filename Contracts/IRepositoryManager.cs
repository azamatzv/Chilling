namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository User {  get; }
    Task SaveAsync();
}