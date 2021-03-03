using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Topic
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
    }
}
