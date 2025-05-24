using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Common.Utilities.Result.Abstract;

namespace Challenge.Common.Utilities.Result.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }
        public int StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with a success flag, message, and status code.
        /// </summary>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="message">A message describing the result of the operation.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the result. Default is 200.</param>
        public Result(bool success, string message, int statusCode = 200)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with a success flag and status code.
        /// </summary>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the result. Default is 200.</param>
        public Result(bool success, int statusCode = 200)
        {
            Success = success;
            StatusCode = statusCode;
        }

    }
}
