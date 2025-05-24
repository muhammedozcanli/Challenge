using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with data, error message, and status code.
        /// </summary>
        /// <param name="data">The data associated with the error result.</param>
        /// <param name="message">A message describing the error.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the error. Default is 400.</param>
        public ErrorDataResult(T data, string message, int statusCode = 400) : base(data, false, message, statusCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with data only.
        /// </summary>
        /// <param name="data">The data associated with the error result.</param>
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with an error message and status code, without any data.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the error. Default is 400.</param>
        public ErrorDataResult(string message, int statusCode = 400) : base(default, false, message, statusCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDataResult{T}"/> class with no data or message, representing a generic error.
        /// </summary>
        public ErrorDataResult() : base(default, false)
        {
        }

    }
}
