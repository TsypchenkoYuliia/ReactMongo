using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure
{
    public class QuestionModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProvider = bindingContext.ValueProvider;

           
            var textPartValues = bindingContext.ValueProvider.GetValue("Text");
            var answersPartValues = bindingContext.ValueProvider.GetValue("Answers");
            var likesPartValues = bindingContext.ValueProvider.GetValue("Likes");
            var dislikesPartValues = bindingContext.ValueProvider.GetValue("Dislikes");
            var topicsPartValues = bindingContext.ValueProvider.GetValue("Topics");

            //var title = titlePartValues.FirstValue;
            var text = textPartValues.FirstValue;
            var answers = answersPartValues.FirstValue;
            var likes = likesPartValues.FirstValue;
            var dislikes = dislikesPartValues.FirstValue;
            var topics = topicsPartValues.FirstValue;

            bindingContext.Result = ModelBindingResult.Success(new Question { 
                //Title = title,
                Text = text
               

            });
            return Task.CompletedTask;
        }
    }
}
