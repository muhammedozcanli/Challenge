using Challenge.API.Examples.Requests;
using Challenge.API.Examples.Responses;
using Challenge.Business.ErrorOperations;
using Challenge.Business.UserOperations;
using Challenge.Common.Constants;
using Challenge.Common.Security;
using Challenge.Common.Utilities.Result.Concrete;
using Challenge.Persistence;
using Challenge.Persistence.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserOperations _userOperations;
        private readonly IErrorOperations _errorOperations;
        private readonly ChallengeDBContext _dbContext;

        public UserController(IUserOperations userOperations, IErrorOperations errorOperations, ChallengeDBContext dbContext)
        {
            _userOperations = userOperations;
            _errorOperations = errorOperations;
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates a user and returns a JWT token")]
        [SwaggerRequestExample(typeof(LoginDTO), typeof(LoginRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
        [ProducesResponseType(typeof(SuccessDataResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<ErrorDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<ErrorDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDataResult<ErrorDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validationError = new ErrorDTO
                    {
                        Name = "ValidationError",
                        Message = Messages.User.ValidationError
                    };
                    _errorOperations.AddError(validationError);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(validationError));
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.FirstName == request.FirstName);

                var hashedPassword = HashingHelper.HashPassword(request.Password);
                if (user == null || user.Password != hashedPassword)
                {
                    var authError = new ErrorDTO
                    {
                        Name = "InvalidCredentialsError",
                        Message = Messages.User.InvalidCredentials
                    };
                    _errorOperations.AddError(authError);
                    return Unauthorized(new ErrorDataResult<ErrorDTO>(authError, Messages.User.InvalidCredentials, 401));
                }

                var token = TokenHelper.GenerateToken(user.Id, user.FirstName);
                
                var responseData = new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    token = token
                };

                return Ok(new SuccessDataResult<object>(responseData, Messages.User.LoginSuccess));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorDTO
            {
                Name = ex.GetType().Name ?? "UnhandledException",
                Message = ex.Message ?? Messages.Error.UnexpectedError
            };

            _errorOperations.AddError(error);

            int statusCode = ex switch
            {
                ArgumentNullException => 400,
                UnauthorizedAccessException => 401,
                KeyNotFoundException => 404,
                _ => 500
            };

            return StatusCode(statusCode, new ErrorDataResult<ErrorDTO>(error, error.Message, statusCode));
        }
    }
}
