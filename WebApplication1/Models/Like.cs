using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Like
    {
        public ObjectId Id { get; set; }
        public string Author { get; set; }
    }
}
