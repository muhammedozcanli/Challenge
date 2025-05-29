using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Requests
{
    public class OrderIdRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            var data = new
            {
                OrderId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            };
            return data;
        }
    }
} 