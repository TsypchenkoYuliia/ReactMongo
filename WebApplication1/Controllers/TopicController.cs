using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories;
using WebApplication1.Context.Repositories.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IRepository<Topic> _repository;

        public TopicController()
        {
            _repository = new TopicRepository();
        }

        [HttpPost]
        public async Task Post([FromBody] IEnumerable<string> topics)
        {
            var currentTopics = await _repository.GetAllAsync();

            var result = topics.Select(x=>x.ToLower()).Except(currentTopics.Select(t=>t.Title.ToLower()));

            result.Select(async (title) => await _repository.InsertOneAsync(new Topic()
            {
                Title = title
            })).ToList();
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var res = (await _repository.GetAllAsync()).Select(x => x.Title);
            return (await _repository.GetAllAsync()).Select(x=>x.Title);
        }
    }
}
