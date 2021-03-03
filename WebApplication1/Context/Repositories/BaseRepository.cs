using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Context.Repositories
{
    public class BaseRepository
    {
        private readonly MongoClient _client;
        protected IMongoDatabase _database;

        public BaseRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("questions_db");
        }
    }
}
