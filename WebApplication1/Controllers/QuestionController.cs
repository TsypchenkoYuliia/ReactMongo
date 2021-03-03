﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public QuestionController()
        {
            _questionRepository = new QuestionRepository();
            _topicRepository = new TopicRepository();
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

    }
}
