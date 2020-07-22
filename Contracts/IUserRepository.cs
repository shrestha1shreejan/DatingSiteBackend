using Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void CreateUser(User user);

        void DeleteUser(User user);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(Guid userid);
    }
}
