using Challenge.Persistence.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API
{
    public static class ResponseHelper
    {
        public static IActionResult Success(object data)
        {
            return new OkObjectResult(new
            {
                success = true,
                data
            });
        }

        public static IActionResult Error(int statusCode, string name, string message)
        {
            var error = new ErrorDTO
            {
                Name = name,
                Message = message
            };

            return new ObjectResult(new
            {
                success = false,
                error
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
