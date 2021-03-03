using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Infrastructure
{
    public class QuestionModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new QuestionModelBinder();
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(Question) ? binder : null;
        }
    }
}
