using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books_api.Data.ViewModels;

namespace my_books_api.ActionResults
{
    public class CustomActionResult<T> : IActionResult
    {
        private readonly CustomActionResultVM<T> _result;
        public CustomActionResult(CustomActionResultVM<T> result)
        {
            _result = result;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result.Exception ?? _result.Data as object)
            {
                StatusCode = _result.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
