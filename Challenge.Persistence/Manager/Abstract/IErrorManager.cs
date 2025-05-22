using Challenge.Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Abstract
{
    public interface IErrorManager
    {
        bool AddError(ErrorDTO errorDTO);
    }
}
