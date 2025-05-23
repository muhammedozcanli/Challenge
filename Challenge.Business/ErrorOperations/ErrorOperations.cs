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
        /// <summary>
        /// Adds an error record to the system for logging or auditing purposes.
        /// </summary>
        /// <param name="errorDTO">The <see cref="ErrorDTO"/> object containing error details.</param>
        /// <returns><c>true</c> if the error was logged successfully; otherwise, <c>false</c>.</returns>
        public bool AddError(ErrorDTO errorDTO)
        {
            return _errorManager.AddError(errorDTO);
        }

    }
}
