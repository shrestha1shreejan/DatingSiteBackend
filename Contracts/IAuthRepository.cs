using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthRepository : IRepositoryBase<User>
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
