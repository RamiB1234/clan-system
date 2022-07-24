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

        public User LoginUser(string userName, string sessionId)
        {
            var foundUser= _users.Find(x => x.UserName == userName).SingleOrDefault();

            // If user not found, add it:
            if(foundUser==null)
            {
                var newUser = new User()
                {
                    UserName = userName,
                    SessionId = sessionId
                };

                _users.InsertOne(newUser);
                return newUser;
            }

            else
            {
                // Update sessionId of the found user:
                foundUser.SessionId = sessionId;
                _users.ReplaceOne(x => x.UserName == foundUser.UserName, foundUser);
                
                return foundUser;
            }
        }

        public bool UserHasAnotherSession(string userName, string sessionId)
        {
            var foundUser = _users.Find(x => x.UserName == userName).SingleOrDefault();
            return foundUser.SessionId == sessionId ? false : true;
        }
    }
}
