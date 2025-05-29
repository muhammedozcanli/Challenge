using Challenge.Common.Utilities.Result.Concrete;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Responses
{
    public class LoginResponseExample : IExamplesProvider<SuccessDataResult<object>>
    {
        public SuccessDataResult<object> GetExamples()
        {
            var data = new
            {
                userId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                firstName = "john.doe",
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
            };

            return new SuccessDataResult<object>(data, "User logged in successfully.");
        }
    }
} 