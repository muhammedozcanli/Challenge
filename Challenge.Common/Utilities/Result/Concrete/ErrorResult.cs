using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Concrete
{
    public class ErrorResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class with an error message and status code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the error. Default is 400.</param>
        public ErrorResult(string message, int statusCode = 400) : base(false, message, statusCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class with no message or status code, representing a generic error.
        /// </summary>
        public ErrorResult() : base(false)
        {
        }

    }
}
