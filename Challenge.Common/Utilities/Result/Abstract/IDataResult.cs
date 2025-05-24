using Challenge.Common.Utilities.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Common.Utilities.Result.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
