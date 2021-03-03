using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Answer
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public DateTime createdDate { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Dislike> Dislikes { get; set; }
    }
}
