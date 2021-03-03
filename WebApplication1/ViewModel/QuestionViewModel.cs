using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class QuestionViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public IEnumerable<string> Topics { get; set; }
    }
}
