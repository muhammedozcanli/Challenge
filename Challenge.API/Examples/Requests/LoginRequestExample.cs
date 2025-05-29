using Challenge.Persistence.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Requests
{
    public class LoginRequestExample : IExamplesProvider<LoginDTO>
    {
        public LoginDTO GetExamples()
        {
            return new LoginDTO
            {
                FirstName = "john.doe",
                Password = "password123"
            };
        }
    }
} 