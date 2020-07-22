using Contracts;
using Entities;
using Entities.Models;
using FileLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private ILoggerManager _logger;

        #region Constructor

        public UserRepository(DataContext repositoryContext, ILoggerManager logger): base(repositoryContext)
        {
            _logger = logger;
        }

        #endregion

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users =  await FindAll().OrderBy(user => user.Username).Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<User> GetUserAsync(Guid userid)
        {
            var user = await FindByCondition(user => user.Id.Equals(userid)).Include(p => p.Photos).FirstOrDefaultAsync();
            return user;
        }


    }
}
