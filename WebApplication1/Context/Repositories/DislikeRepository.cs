using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Context.Repositories
{
    public class DislikeRepository : BaseRepository, IRepository<Dislike>
    {
        private IMongoCollection<Dislike> _dislikes;

        public DislikeRepository() : base()
        {
            _dislikes = _database.GetCollection<Dislike>("Dislikes");
        }
        public async Task<IEnumerable<Dislike>> GetAllAsync()
        {
            return await _dislikes.AsQueryable<Dislike>().ToListAsync();
        }

        public Task<Dislike> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertOneAsync(Dislike obj)
        {
            await _dislikes.InsertOneAsync(obj);
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<Dislike>.Filter.Eq("Id", id);
            var res = await _dislikes.FindSync(filter).FirstOrDefaultAsync();
            await _dislikes.DeleteOneAsync(filter);
        }

        public async Task<Dislike> GetByAuthorAsync(string authorName)
        {
            var filter = Builders<Dislike>.Filter.Eq("Author", authorName);
            return await _dislikes.FindSync(filter).FirstOrDefaultAsync();
        }
    }
}
