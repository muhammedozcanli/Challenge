using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Concrete;

public class SuccessResult : Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResult"/> class with a success message.
    /// </summary>
    /// <param name="message">A message describing the successful operation.</param>
    public SuccessResult(string message) : base(true, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResult"/> class with no message, representing a generic successful result.
    /// </summary>
    public SuccessResult() : base(true)
    {
    }

}
