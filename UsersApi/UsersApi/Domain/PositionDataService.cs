﻿using System.Collections.Generic;
using MongoDB.Driver;

namespace UsersApi.Domain
{
    public class PositionDataService : IPositionDataService
    {
        public Position GetById(string id)
        {
            var client = new MongoClient("mongodb://admin:admin@localhost:27017");
            var db = client.GetDatabase("users");
            var collection = db.GetCollection<Position>("positions");
            return collection
                .Find(p => p.Id == id)
                .FirstOrDefault();
        }
        public List<Position> GetAll()
        {
            var client = new MongoClient("mongodb://admin:admin@localhost:27017");
            var db = client.GetDatabase("users");
            var collection = db.GetCollection<Position>("positions");
            return collection
                .Find(p => true)
                .ToList();
        }
    }
}