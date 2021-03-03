using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Context.Repositories
{
    public class QuestionRepository : BaseRepository, IRepository<Question>
    {
        private IMongoCollection<Question> _questions;
        private TopicRepository _topicRepository;
        public QuestionRepository() : base()
        {
            _questions = _database.GetCollection<Question>("Questions");
            _topicRepository = new TopicRepository();
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _questions.AsQueryable<Question>().ToListAsync();
        }

        public async Task InsertOneAsync(Question obj)
        {
           await _questions.InsertOneAsync(obj);
        }

        public async Task<IEnumerable<Question>> GetByTopicAsync(string topic)
        {
            var top = await _topicRepository.GetByFieldAsync("Title", topic);
            var filter = Builders<Question>.Filter.Where(x => x.Topics.Contains(top));
            return await _questions.FindAsync(filter).Result.ToListAsync();
        }
    }
}
