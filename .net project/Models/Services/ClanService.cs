using clan_system.Models.Entities;
using MongoDB.Driver;
using System.Collections.Generic;

namespace clan_system.Models.Services
{
    public class ClanService
    {
        private readonly IMongoCollection<Clan> _clans;

        public ClanService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _clans = database.GetCollection<Clan>("Clans");
        }

        public List<Clan> GetClanList()
        {
            return _clans.Find(x => true).ToList();
        }
    }
}
