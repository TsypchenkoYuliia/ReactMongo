using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Context.Repositories
{
    public class AnswerRepository : BaseRepository, IRepository<Answer>
    {
        private IMongoCollection<Answer> _answers;

        public AnswerRepository() : base()
        {
            _answers = _database.GetCollection<Answer>("Answers");
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _answers.AsQueryable<Answer>().ToListAsync();
        }

        public async Task<Answer> GetByIdAsync(string id)
        {
            var filter = Builders<Answer>.Filter.Eq("Id", id);
            return await _answers.FindSync(filter).FirstOrDefaultAsync();
        }

        public async Task InsertOneAsync(Answer obj)
        {
            await _answers.InsertOneAsync(obj);
        }
    }
}
