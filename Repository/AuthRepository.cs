using Contracts;
using Entities;
using Entities.Models;
using FileLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthRepository : RepositoryBase<User>, IAuthRepository
    {
        private ILoggerManager _logger;

        #region Constructor

        public AuthRepository(DataContext context, ILoggerManager logger) : base(context)
        {
            _logger = logger;
        }

        #endregion


        #region Implementation
        public async Task<User> Login(string username, string password)
        {
            _logger.LogInfo($"Login method executed for user with username: {username}");
            var user = await FindByCondition(user => user.Username.Equals(username)).FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.LogError($"No such user with username: {username} exists");
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                _logger.LogError($"Password validation error");
                return null;
            }

            _logger.LogInfo($"Login successful for user with username: {username}");
            return user;
        }     

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordhash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            Create(user);
            await SaveAsync();
            _logger.LogInfo($"sucessfully registerd user with username: {user.Username}");
            return user;
        }       

        public async Task<bool> UserExists(string username)
        {
            if (await FindByCondition(x => x.Username == username).AnyAsync())
            {
                _logger.LogInfo($"User with matching username: {username} found ");
                return true;
            }

            return false;
        }

        #endregion

        #region Private

        private void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // IDisposable (Dispose the Security object as soon as the operation is done)
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

    }
}
