using PostgreSqlDotnetCore.Models;
using System;
namespace PostgreSqlDotnetCore.repositories
{
    public class UserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            // Initialize the user data
            _users = new List<User>
            {
                new User { user_id = 1, username = "admin", password = "password" }
            };
        }

        public bool AuthenticateUser(User user)
        {
            // Authenticate the user against the stored data
            var storedUser = _users.FirstOrDefault(u => u.username == user.username && u.password == user.password);
            return storedUser != null;
        }
    }
}

