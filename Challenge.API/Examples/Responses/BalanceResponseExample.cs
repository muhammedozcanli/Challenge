using Challenge.Common.Utilities.Result.Concrete;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Examples.Responses
{
    public class BalanceResponseExample : IExamplesProvider<SuccessDataResult<object>>
    {
        public SuccessDataResult<object> GetExamples()
        {
            var data = new
            {
                availableBalance = 1000.00,
                blockedBalance = 50.00,
                Currency = "USD",
                LastUpdated = DateTime.UtcNow
            };

            return new SuccessDataResult<object>(data, "Balance retrieved successfully.");
        }
    }
} 