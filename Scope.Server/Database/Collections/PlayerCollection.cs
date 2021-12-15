using System;
using LiteDB;

namespace Scope.Server.Database.Collections
{
    public class PlayerCollection
    {
        [BsonCtor]
        public Player(string id, string authentication, string name, string ipAddress, string hwid, DateTime firstJoin, DateTime lastJoin)
        {
            Id = id;
            Authentication = authentication;
            Name = name;
            IpAddress = ipAddress;
            Hwid = hwid;
            FirstJoin = firstJoin;
            LastJoin = lastJoin;
        }

        public string Id { get; }
        public string Authentication { get; }
        public string Name { get; internal set; }
        public string IpAddress { get; internal set; }
        public string Hwid { get; }
        public DateTime FirstJoin { get; }
        public DateTime LastJoin { get; set; }
    }
}
