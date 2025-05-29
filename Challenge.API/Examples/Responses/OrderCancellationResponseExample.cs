using Challenge.Common.Utilities.Result.Concrete;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Responses
{
    public class OrderCancellationResponseExample : IExamplesProvider<SuccessDataResult<object>>
    {
        public SuccessDataResult<object> GetExamples()
        {
            var data = new
            {
                OrderId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                CancelledAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return new SuccessDataResult<object>(data, "Order cancelled successfully.");
        }
    }
} 