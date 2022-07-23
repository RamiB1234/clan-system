using clan_system.Models.Entities;
using MongoDB.Driver;
using System;

namespace clan_system.Models.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public User LoginUser(string userName)
        {
            var foundUser= _users.Find(x => x.UserName == userName).SingleOrDefault();

            // If user not found, add it:
            if(foundUser==null)
            {
                var newUser = new User()
                {
                    UserName = userName
                };

                _users.InsertOne(newUser);
                return newUser;
            }

            else
            {
                return foundUser;
            }
        }
    }
}
