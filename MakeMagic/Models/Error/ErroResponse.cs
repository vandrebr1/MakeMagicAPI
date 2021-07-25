using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace MakeMagic.Models.Error
{
    public class ErroResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string[] Details { get; set; }
        public ErroResponse InnerError { get; set; }

        public static ErroResponse From(System.Exception e)
        {
            if (e == null)
            {
                return null;
            }
            return new ErroResponse
            {
                Code = e.HResult,
                Message = e.Message,
                InnerError = From(e.InnerException)
            };
        }

        public static ErroResponse FromModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);
            return new ErroResponse
            {
                Code = 100,
                Message = "There was an error(s) in the validation of the request",
                Details = errors.Select(e => e.ErrorMessage).ToArray(),
            };
        }
    }
}
