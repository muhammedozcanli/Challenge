using Challenge.Persistence.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API
{
    public static class ResponseHelper
    {
        /// <summary>
        /// Returns a successful HTTP 200 (OK) response with the specified data payload.
        /// </summary>
        /// <param name="data">The object to include in the response data.</param>
        /// <returns>An <see cref="IActionResult"/> representing a successful result with the provided data.</returns>
        public static IActionResult Success(object data)
        {
            return new OkObjectResult(new
            {
                success = true,
                data
            });
        }
        /// <summary>
        /// Returns an error response with a custom status code and error details.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to return.</param>
        /// <param name="name">A short name or code identifying the error type.</param>
        /// <param name="message">A descriptive message explaining the error.</param>
        /// <returns>An <see cref="IActionResult"/> representing a failed result with error information.</returns>
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
