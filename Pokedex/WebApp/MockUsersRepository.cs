using System.Collections.Generic;
using System.Linq;
using PokedexBackend.DataAccess.EfModels;

namespace WebApp.Repositories
{
    public class UsersRepository
    {
        private readonly List<User> _users;

        public UsersRepository()
        {
            _users = new List<User>
            {
                new User { Username = "user1", Password = "password1" },
                new User { Username = "user2", Password = "password2" },
            };
        }

        public User Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(user => user.Username == username && user.Password == password);
        }
    }
}