using Challenge.API.Examples.Responses;
using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Business.ProductOperations;
using Challenge.Common.Constants;
using Challenge.Common.Utilities.Result.Concrete;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Challenge.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductOperations _productOperations;
        private readonly IErrorOperations _errorOperations;

        public ProductController(IProductOperations productOperations, IErrorOperations errorOperations)
        {
            _productOperations = productOperations;
            _errorOperations = errorOperations;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get products and prices", Description = "Retrieves a list of all available products with their prices")]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ProductResponseExample))]
        [ProducesResponseType(typeof(List<ProductDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productOperations.GetProducts();

                if (products == null || !products.Any())
                {
                    var error = new ErrorDTO
                    {
                        Name = "ProductNotFoundException",
                        Message = Messages.Product.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.Product.NotFound));
                }

                var data = products.Select(product => new
                {
                    id = product.Id,
                    name = product.Name,
                    description = product.Description,
                    price = product.Price,
                    currency = product.Currency,
                    category = product.Category,
                    stock = product.Stock
                });

                return Ok(new SuccessDataResult<object>(data, Messages.Product.Retrieved));
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
