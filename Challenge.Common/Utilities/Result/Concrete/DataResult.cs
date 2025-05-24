using Challenge.Common.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Concrete
{

    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataResult{T}"/> class with data, success flag, message, and status code.
        /// </summary>
        /// <param name="data">The data returned by the operation.</param>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="message">A message describing the result of the operation.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the result. Default is 200.</param>
        public DataResult(T data, bool success, string message, int statusCode = 200)  : base(success, message, statusCode)
        {
            Data = data;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataResult{T}"/> class with data, success flag, and status code.
        /// </summary>
        /// <param name="data">The data returned by the operation.</param>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="statusCode">An optional HTTP-style status code representing the result. Default is 200.</param>
        public DataResult(T data, bool success, int statusCode = 200)  : base(success, statusCode)
        {
            Data = data;
        }
    }

}
