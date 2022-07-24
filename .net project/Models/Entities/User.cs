using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace clan_system.Models.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }
        public bool InClan { get; set; }

        public int Contribution { get; set; } = 0;
    }
}
