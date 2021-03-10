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
        private IMongoCollection<Like> _likes;
        private IMongoCollection<Dislike> _dislikes;

        private TopicRepository _topicRepository;
        public QuestionRepository() : base()
        {
            _questions = _database.GetCollection<Question>("Questions");
            _likes = _database.GetCollection<Like>("Likes");
            _dislikes = _database.GetCollection<Dislike>("Dislikes");
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

            var questions = await _questions.FindAsync(filter).Result.ToListAsync();

            foreach(var item in questions)
            {
                item.Likes = await _likes.FindSync(Builders<Like>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                item.Dislikes = await _dislikes.FindSync(Builders<Dislike>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
            }

            return questions;
        }

        public async Task<IEnumerable<Question>> GetByRatingAsync(string rating)
        {
            switch (rating)
            {
                case "popular":
                    var popularFilter = Builders<Question>.Filter.Where(x => x.Answers.Count() > 1);
                    var popularQuestions = await _questions.FindAsync(popularFilter).Result.ToListAsync();
                    foreach (var item in popularQuestions)
                    {
                        item.Likes = await _likes.FindSync(Builders<Like>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                        item.Dislikes = await _dislikes.FindSync(Builders<Dislike>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                    }
                    return popularQuestions;
                case "unanswered":
                    var unansweredFilter = Builders<Question>.Filter.Where(x => x.Answers.Count() == 0);
                    var unansweredQuestions = await _questions.FindAsync(unansweredFilter).Result.ToListAsync();
                    foreach (var item in unansweredQuestions)
                    {
                        item.Likes = await _likes.FindSync(Builders<Like>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                        item.Dislikes = await _dislikes.FindSync(Builders<Dislike>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                    }
                    return unansweredQuestions;
                default:
                    var questions = await this.GetAllAsync();
                    foreach (var item in questions)
                    {
                        item.Likes = await _likes.FindSync(Builders<Like>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                        item.Dislikes = await _dislikes.FindSync(Builders<Dislike>.Filter.Where(x => x.QuestionId == item.Id)).ToListAsync();
                    }
                    return questions;
            }
        }

        public async Task<Question> GetByIdAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq("Id", id);
            var question = await _questions.FindSync(filter).FirstOrDefaultAsync();

            question.Likes = await _likes.FindSync(Builders<Like>.Filter.Where(x=>x.QuestionId == id)).ToListAsync();
            question.Dislikes = await _dislikes.FindSync(Builders<Dislike>.Filter.Where(x => x.QuestionId == id)).ToListAsync();

            return question;
        }

        public async Task AddAnswerAsync(string id, Answer answer)
        {
            var filter = Builders<Question>.Filter.Eq("Id", id);
            var question =  await _questions.FindSync(filter).FirstOrDefaultAsync();
            question.Answers.Add(answer);

            await _questions.ReplaceOneAsync(filter, question);
        }

        public async Task AddLikeAsync(string id, Like like)
        {
            var filter = Builders<Question>.Filter.Eq("Id", id);
            var question = await _questions.FindSync(filter).FirstOrDefaultAsync();
            question.Likes.Add(like);

            await _questions.ReplaceOneAsync(filter, question);
        }

        public async Task AddDisLikeAsync(string id, Dislike dislike)
        {
            var filter = Builders<Question>.Filter.Eq("Id", id);
            var question = await _questions.FindSync(filter).FirstOrDefaultAsync();
            question.Dislikes.Add(dislike);

            await _questions.ReplaceOneAsync(filter, question);
        }

        public async Task<IEnumerable<Answer>> GetAnswersAsync(string questionId)
        {
            var filter = Builders<Question>.Filter.Eq("Id", questionId);
            return (await _questions.FindSync(filter).FirstOrDefaultAsync()).Answers;
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<Question>.Filter.Eq("Id", id);
            await _questions.DeleteOneAsync(filter);
        }

        
    }
}
