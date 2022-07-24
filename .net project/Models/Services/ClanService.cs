using clan_system.Models.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace clan_system.Models.Services
{
    public class ClanService
    {
        private readonly IMongoCollection<Clan> _clans;
        private readonly IConfiguration _config;

        public ClanService(IDatabaseSettings settings, IConfiguration config)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clans = database.GetCollection<Clan>("Clans");
            _config = config;
        }

        public List<Clan> GetClanList()
        {
            // This method checks if clan collection is set up
            // If not, it inserts the clan information from the config file

            var clanList = _clans.Find(x => true).ToList();
            if(clanList.Count==0)
            {
                clanList = _config.GetSection("ClanList").Get<List<Clan>>();
                _clans.InsertMany(clanList);
            }

            return clanList;
        }

        public void JoinClan(string userName, string clanName)
        {
            var clan = _clans.Find(x => x.Name == clanName).SingleOrDefault();

            var user = new User()
            {
                UserName = userName,
                InClan = true
            };

            // Initialize array if not already initialized:
            if(clan.Users==null)
            {
                clan.Users = new List<User>();
            }

            clan.Users.Add(user);

            _clans.ReplaceOne(x => x.Id == clan.Id, clan);
        }

        public void LeaveClan(string userName, string clanName)
        {
            var clan = _clans.Find(x => x.Name == clanName).SingleOrDefault();
            var user = clan.Users.Find(x => x.UserName == userName);

            clan.Users.Remove(user);

            user.InClan = false;

            clan.Users.Add(user);

            _clans.ReplaceOne(x => x.Id == clan.Id, clan);
        }
    }
}
