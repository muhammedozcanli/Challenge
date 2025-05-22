using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.ErrorOperations
{
    public interface IErrorOperations
    {
        bool AddError(ErrorDTO errorDTO);
    }
}
