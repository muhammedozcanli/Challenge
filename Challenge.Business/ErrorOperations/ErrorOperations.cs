using Challenge.Persistence.DTOs;
using Challenge.Persistence.Manager.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Business.ErrorOperations
{
    public class ErrorOperations : IErrorOperations
    {
        IErrorManager _errorManager;
        public ErrorOperations(IErrorManager errorManager)
        {
            _errorManager = errorManager;
        }
        public bool AddError(ErrorDTO errorDTO)
        {
            return _errorManager.AddError(errorDTO);
        }
    }
}
