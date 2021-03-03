using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Context.Repositories
{
    public class TopicRepository : BaseRepository, IRepository<Topic>
    {
        IMongoCollection<Topic> _topics;
        public TopicRepository()
        {
            _topics = _database.GetCollection<Topic>("Topics");
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _topics.AsQueryable<Topic>().ToListAsync();
        }

        public async Task InsertOneAsync(Topic obj)
        {
            await _topics.InsertOneAsync(obj);
        }

        public async Task<Topic> GetByFieldAsync(string field, string value)
        {
            var filter = Builders<Topic>.Filter.Eq(field, value);
            return await _topics.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
    }
}
