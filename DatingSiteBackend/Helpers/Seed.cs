using Entities;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DatingSiteBackend.Helpers
{
    public class Seed
    {
        public static void SeedUser(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = File.ReadAllText("SeedData/UserSeedData.json");
                // Deserializing the json data to get list of User
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordhash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }


        #region Private methods

        private static void CreatePasswordhash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // IDisposable (Dispose the Security object as soon as the operation is done)
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        #endregion
    }
}
