using MakeMagic.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MakeMagic.Filters
{
    public class ErroResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(ErroResponse.From(context.Exception)) { StatusCode = 500 };
        }
    }
}
