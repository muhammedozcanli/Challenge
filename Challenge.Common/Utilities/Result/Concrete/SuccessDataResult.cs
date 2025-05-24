using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with data and a success message.
        /// </summary>
        /// <param name="data">The data returned by the successful operation.</param>
        /// <param name="message">A message describing the success.</param>
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with data only.
        /// </summary>
        /// <param name="data">The data returned by the successful operation.</param>
        public SuccessDataResult(T data) : base(data, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with a success message but no data.
        /// </summary>
        /// <param name="message">A message describing the success.</param>
        public SuccessDataResult(string message) : base(default, true, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDataResult{T}"/> class with no data or message, representing a generic successful result.
        /// </summary>
        public SuccessDataResult() : base(default, true)
        {
        }

    }
}

