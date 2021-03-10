using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context.Repositories;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly QuestionRepository _questionRepository;
        private readonly AnswerRepository _answerRepository;

        public AnswerController()
        {
            _questionRepository = new QuestionRepository();
            _answerRepository = new AnswerRepository();
        }

        [HttpPost("{questionId}")]
        public async Task Post(string questionId, [FromBody] Answer model)
        {
            model.CreatedDate = DateTime.Now.Date;
            model.Dislikes = new List<Dislike>();
            model.Likes = new List<Like>();
            await _answerRepository.InsertOneAsync(model);
            await _questionRepository.AddAnswerAsync(questionId, model);
        }

        [HttpGet("{questionId}")]
        public async Task<IEnumerable<Answer>> Get(string questionId)
        {
            return await _questionRepository.GetAnswersAsync(questionId);
        }
    }
}
