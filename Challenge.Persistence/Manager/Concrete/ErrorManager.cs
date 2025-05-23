using AutoMapper;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Persistence.Manager.Concrete
{
    public class ErrorManager :  IErrorManager
    {
        IErrorRepository _errorRepository;
        IMapper _mapper;
        public ErrorManager(IErrorRepository errorRepository,IMapper mapper)
        {
            _errorRepository = errorRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Adds a new error record to the database using the provided <see cref="ErrorDTO"/>.
        /// </summary>
        /// <param name="errorDTO">The data transfer object representing the error details.</param>
        /// <returns>True if the error is successfully added; otherwise, false.</returns>
        public bool AddError(ErrorDTO errorDTO)
        {
            try
            {
                var error = _mapper.Map<Error>(errorDTO);
                _errorRepository.Add(error);
                _errorRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
