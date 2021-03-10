using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Context.Repositories
{
    public class LikeRepository : BaseRepository, IRepository<Like>
    {
        private IMongoCollection<Like> _likes;

        public LikeRepository() : base()
        {
            _likes = _database.GetCollection<Like>("Likes");
        }
        public async Task<IEnumerable<Like>> GetAllAsync()
        {
            return await _likes.AsQueryable<Like>().ToListAsync();
        }

        public Task<Like> GetByIdAsync(string id)
        {
            return null;
        }

        public async Task InsertOneAsync(Like obj)
        {
            await _likes.InsertOneAsync(obj);
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<Like>.Filter.Eq("Id", id);
            var res = await _likes.FindSync(filter).FirstOrDefaultAsync();
            await _likes.DeleteOneAsync(filter);
        }

        public async Task<Like> GetByAuthorAsync(string authorName)
        {
            var filter = Builders<Like>.Filter.Eq("Author", authorName);
            return await _likes.FindSync(filter).FirstOrDefaultAsync();
        }
    }
}
