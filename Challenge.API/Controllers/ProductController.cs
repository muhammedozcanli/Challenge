using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Business.ProductOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductOperations _productOperations;
        IErrorOperations _errorOperations;
        public ProductController(IProductOperations productOperations, IErrorOperations errorOperations)
        {
            _productOperations = productOperations;
            _errorOperations = errorOperations;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get products and prices", Description = "Retrieves a list of all available products with their prices")]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productOperations.GetProducts();

                if (products == null || !products.Any())
                    return NotFoundWithError("No products found");

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

                return ResponseHelper.Success(data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        private IActionResult NotFoundWithError(string message)
        {
            var error = new ErrorDTO { Name = "NotFound", Message = message };
            _errorOperations.AddError(error);
            return ResponseHelper.Error(404, error.Name, error.Message);
        }

        private IActionResult HandleException(Exception ex)
        {
            var error = new ErrorDTO
            {
                Name = ex.GetType().Name ?? "UnhandledException",
                Message = ex.Message ?? "An unexpected error occurred."
            };

            _errorOperations.AddError(error);

            int statusCode = ex switch
            {
                ArgumentNullException => 400,
                UnauthorizedAccessException => 401,
                KeyNotFoundException => 404,
                _ => 500
            };

            return ResponseHelper.Error(statusCode, error.Name, error.Message);
        }

    }
}
