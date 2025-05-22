using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Business.ProductOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.API.Controllers
{
    [Route("api/[controller]")]
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
                {
                    var error = new ErrorDTO
                    {
                        Name = "NotFound",
                        Message = "No products found"
                    };

                    _errorOperations.AddError(error);

                    return ResponseHelper.Error(404, error.Name, error.Message);
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

                return ResponseHelper.Success(data);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO
                {
                    Name = ex.GetType().Name,
                    Message = ex.Message
                };

                _errorOperations.AddError(error);

                return ResponseHelper.Error(
                    statusCode: ex switch
                    {
                        ArgumentNullException => 400,
                        UnauthorizedAccessException => 401,
                        KeyNotFoundException => 404,
                        _ => 500
                    },
                    name: error.Name ?? "UnhandledException",
                    message: error.Message ?? "An unexpected error occurred."
                );
            }
        }

    }
}
