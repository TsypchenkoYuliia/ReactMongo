using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionRepository _questionRepository;
        private readonly TopicRepository _topicRepository;
        private readonly LikeRepository _likeRepository;
        private readonly DislikeRepository _dislikeRepository;

        public QuestionController()
        {
            _questionRepository = new QuestionRepository();
            _topicRepository = new TopicRepository();
            _likeRepository = new LikeRepository();
            _dislikeRepository = new DislikeRepository();
        }

        [HttpPost]
        public async Task Post([FromBody] QuestionViewModel model)
        {
            //var list = new List<Topic>();

            var reslist = model.Topics.Select(async x => (await _topicRepository.GetByFieldAsync("Title", x))).Select( y => new Topic() { Title = y.Result.Title, Id = y.Result.Id});

            //foreach (var item in model.Topics)
            //{
            //    var topic = await _topicRepository.GetByFieldAsync("Title", item);
            //    list.Add(topic);
            //}

            await _questionRepository.InsertOneAsync(new Question()
            {
                CreatedDate = DateTime.Now.Date,
                Title = model.Title,
                Text = model.Text,
                Topics = reslist,
                Answers = new List<Answer>(),
                Likes = new List<Like>(),
                Dislikes = new List<Dislike>(),
                Author = model.Author
            });
        }

        [HttpGet]
        public async Task<IEnumerable<Question>> Get()
        {
            return await _questionRepository.GetAllAsync();
        }

        [HttpGet("topic/{topic}")]
        public async Task<IEnumerable<Question>> Get(string topic)
        {
            return await _questionRepository.GetByTopicAsync(topic);
        }

        [HttpGet("rating/{rating}")]
        public async Task<IEnumerable<Question>> GetRating(string rating)
        {
            return await _questionRepository.GetByRatingAsync(rating);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<Question> GetById(string id)
        {
            return await _questionRepository.GetByIdAsync(id);
        }

        [HttpGet("votes/{id}")]
        public async Task<int> GetVotes(string id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            return question.Likes.Count() - question.Dislikes.Count();
        }

        [HttpPost("like/{id}")]
        public async Task<HttpStatusCode> Like(string id, Like like)
        {
            like.QuestionId = id;
            var currentLike = await _likeRepository.GetByAuthorAsync(like.Author);

            if (currentLike == null)
                await _likeRepository.InsertOneAsync(like);
            else
                return HttpStatusCode.BadRequest;

            var dislike = await _dislikeRepository.GetByAuthorAsync(like.Author);
            if (dislike != null)
                await _dislikeRepository.RemoveAsync(dislike.Id);

            return HttpStatusCode.OK;
        }

        [HttpPost("dislike/{id}")]
        public async Task<HttpStatusCode> Dislike(string id, Dislike dislike)
        {
            dislike.QuestionId = id;
            var currentDislike = await _dislikeRepository.GetByAuthorAsync(dislike.Author);

            if (currentDislike == null)
                await _dislikeRepository.InsertOneAsync(dislike);
            else
                return HttpStatusCode.BadRequest;

            var like = await _likeRepository.GetByAuthorAsync(dislike.Author);
            if (like != null)
                await _likeRepository.RemoveAsync(like.Id);

            return HttpStatusCode.OK;
        }
    }

    
}
