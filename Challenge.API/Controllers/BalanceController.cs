using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Common.Constants;
using Challenge.Common.Utilities.Result.Concrete;
using Challenge.Persistence;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Challenge.API.Examples;

namespace Challenge.API.Controllers
{
    [Authorize]
    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceOperations _balanceOperations;
        private readonly IErrorOperations _errorOperations;
        private readonly ChallengeDBContext _dbContext;

        public BalanceController(IBalanceOperations balanceOperations, IErrorOperations errorOperations, ChallengeDBContext dBContext)
        {
            _dbContext = dBContext;
            _balanceOperations = balanceOperations;
            _errorOperations = errorOperations;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get user balance", Description = "Retrieves the current balance information for the user")]
        public IActionResult GetBalance()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "UnauthorizedAccessError",
                        Message = Messages.Authentication.UserIdNotFound
                    };
                    _errorOperations.AddError(error);
                    return Unauthorized(new ErrorDataResult<ErrorDTO>(error, Messages.Authentication.UserIdNotFound, 401));
                }

                var userId = Guid.Parse(userIdClaim.Value);
                var balance = _balanceOperations.GetBalanceByUserId(userId);
                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceNotFoundException",
                        Message = Messages.Balance.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.NotFound, 404));
                }

                var data = new
                {
                    availableBalance = balance.AvailableBalance,
                    blockedBalance = balance.BlockedBalance,
                    balance.Currency,
                    balance.LastUpdated
                };

                return Ok(new SuccessDataResult<object>(data, Messages.Balance.Retrieved));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("preorder")]
        [SwaggerOperation(Summary = "Create a pre-order", Description = "Creates a pre-order and blocks the specified amount from the available balance")]
        [SwaggerRequestExample(typeof(PreOrderDTO), typeof(PreOrderRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PreOrderResponseExample))]
        [ProducesResponseType(typeof(PreOrderResponseDTO), StatusCodes.Status200OK)]
        public IActionResult CreatePreOrder([FromBody] PreOrderDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = new ErrorDTO
                    {
                        Name = "ValidationError",
                        Message = Messages.Balance.ValidationError
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.ValidationError, 400));
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "UnauthorizedAccessError",
                        Message = Messages.Authentication.UserIdNotFound
                    };
                    _errorOperations.AddError(error);
                    return Unauthorized(new ErrorDataResult<ErrorDTO>(error, Messages.Authentication.UserIdNotFound, 401));
                }

                var userId = Guid.Parse(userIdClaim.Value);
                
                // Calculate total amount and validate stocks
                double totalAmount = 0;
                foreach (var product in request.Products)
                {
                    var dbProduct = _dbContext.Products.FirstOrDefault(p => p.Id == product.ProductId);
                    if (dbProduct == null)
                    {
                        var error = new ErrorDTO
                        {
                            Name = "ProductNotFoundException",
                            Message = string.Format(Messages.PreOrder.ProductNotFound, product.ProductId)
                        };
                        _errorOperations.AddError(error);
                        return BadRequest(new ErrorDataResult<ErrorDTO>(error, string.Format(Messages.PreOrder.ProductNotFound, product.ProductId), 400));
                    }
                    
                    if (dbProduct.Stock < product.Quantity)
                    {
                        var error = new ErrorDTO
                        {
                            Name = "InsufficientStockError",
                            Message = string.Format(Messages.PreOrder.InsufficientStock, dbProduct.Name, dbProduct.Stock, product.Quantity)
                        };
                        _errorOperations.AddError(error);
                        return BadRequest(new ErrorDataResult<ErrorDTO>(error, string.Format(Messages.PreOrder.InsufficientStock, dbProduct.Name, dbProduct.Stock, product.Quantity), 400));
                    }
                    
                    totalAmount += (double)(dbProduct.Price ?? 0) * product.Quantity;
                }

                var balance = _balanceOperations.GetBalanceByUserId(userId);
                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceNotFoundException",
                        Message = Messages.Balance.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.NotFound, 404));
                }

                if (balance.AvailableBalance < (double)totalAmount)
                {
                    var error = new ErrorDTO
                    {
                        Name = "InsufficientBalanceError",
                        Message = Messages.Balance.InsufficientBalance
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.InsufficientBalance, 400));
                }

                // Create PreOrder
                var preOrder = new PreOrder
                {
                    Amount = totalAmount,
                    Status = "PreOrder created!",
                    CreatedDate = DateTime.UtcNow,
                    UserId = userId
                };

                _dbContext.PreOrders.Add(preOrder);

                // Create PreOrderProducts and update stocks
                foreach (var product in request.Products)
                {
                    var dbProduct = _dbContext.Products.First(p => p.Id == product.ProductId);
                    
                    var preOrderProduct = new PreOrderProduct
                    {
                        PreOrderId = preOrder.Id,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity
                    };
                    
                    dbProduct.Stock -= product.Quantity;
                    _dbContext.PreOrderProducts.Add(preOrderProduct);
                }

                // Update balance
                balance.BlockedBalance += totalAmount;
                balance.AvailableBalance -= totalAmount;
                balance.LastUpdated = DateTime.UtcNow;

                if (!_balanceOperations.UpdateBalance(balance))
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceUpdateError",
                        Message = Messages.Balance.UpdateFailed
                    };
                    _errorOperations.AddError(error);
                    return StatusCode(500, new ErrorDataResult<ErrorDTO>(error, Messages.Balance.UpdateFailed, 500));
                }

                _dbContext.SaveChanges();

                var response = new
                {
                    preOrder.Id,
                    Amount = totalAmount
                };

                return Ok(new SuccessDataResult<object>(response, Messages.PreOrder.Created));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("complete")]
        [SwaggerOperation(Summary = "Complete an order", Description = "Completes a pre-order by deducting the blocked amount from the total balance")]
        public IActionResult CompleteOrder([FromBody] Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                {
                    var error = new ErrorDTO
                    {
                        Name = "InvalidOrderIdError",
                        Message = Messages.PreOrder.InvalidOrderId
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.InvalidOrderId, 400));
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "UnauthorizedAccessError",
                        Message = Messages.Authentication.UserIdNotFound
                    };
                    _errorOperations.AddError(error);
                    return Unauthorized(new ErrorDataResult<ErrorDTO>(error, Messages.Authentication.UserIdNotFound, 401));
                }

                var userId = Guid.Parse(userIdClaim.Value);
                var preOrder = _dbContext.PreOrders
                    .Include(po => po.PreOrderProducts)
                    .ThenInclude(pop => pop.Product)
                    .FirstOrDefault(po => po.Id == orderId && po.UserId == userId);
                    
                if (preOrder == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "PreOrderNotFoundException",
                        Message = Messages.PreOrder.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.NotFound, 404));
                }

                if (preOrder.CompletedAt != null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "PreOrderAlreadyCompletedError",
                        Message = Messages.PreOrder.AlreadyCompleted
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.AlreadyCompleted, 400));
                }

                var balance = _balanceOperations.GetBalanceByUserId(userId);
                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceNotFoundException",
                        Message = Messages.Balance.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.NotFound, 404));
                }

                balance.BlockedBalance -= preOrder.Amount;
                balance.LastUpdated = DateTime.UtcNow;

                if (!_balanceOperations.UpdateBalance(balance))
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceUpdateError",
                        Message = Messages.Balance.UpdateFailed
                    };
                    _errorOperations.AddError(error);
                    return StatusCode(500, new ErrorDataResult<ErrorDTO>(error, Messages.Balance.UpdateFailed, 500));
                }

                preOrder.Status = "Completed";
                preOrder.CompletedAt = DateTime.UtcNow;

                _dbContext.PreOrders.Update(preOrder);
                _dbContext.SaveChanges();

                var response = new
                {
                    OrderId = preOrder.Id,
                    CompletedAt = preOrder.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return Ok(new SuccessDataResult<object>(response, Messages.PreOrder.Completed));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("cancel")]
        [SwaggerOperation(Summary = "Cancel a pre-order", Description = "Cancels a pre-order and returns the blocked amount to the available balance")]
        public IActionResult CancelOrder([FromBody] Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                {
                    var error = new ErrorDTO
                    {
                        Name = "InvalidOrderIdError",
                        Message = Messages.PreOrder.InvalidOrderId
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.InvalidOrderId, 400));
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "UnauthorizedAccessError",
                        Message = Messages.Authentication.UserIdNotFound
                    };
                    _errorOperations.AddError(error);
                    return Unauthorized(new ErrorDataResult<ErrorDTO>(error, Messages.Authentication.UserIdNotFound, 401));
                }

                var userId = Guid.Parse(userIdClaim.Value);
                var preOrder = _dbContext.PreOrders
                    .Include(po => po.PreOrderProducts)
                    .ThenInclude(pop => pop.Product)
                    .FirstOrDefault(po => po.Id == orderId && po.UserId == userId);
                    
                if (preOrder == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "PreOrderNotFoundException",
                        Message = Messages.PreOrder.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.NotFound, 404));
                }

                if (preOrder.CompletedAt != null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "PreOrderAlreadyCompletedError",
                        Message = Messages.PreOrder.AlreadyCompletedCantCancel
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.AlreadyCompletedCantCancel, 400));
                }

                if (preOrder.CancelledAt != null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "PreOrderAlreadyCancelledError",
                        Message = Messages.PreOrder.AlreadyCancelled
                    };
                    _errorOperations.AddError(error);
                    return BadRequest(new ErrorDataResult<ErrorDTO>(error, Messages.PreOrder.AlreadyCancelled, 400));
                }

                var balance = _balanceOperations.GetBalanceByUserId(userId);
                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceNotFoundException",
                        Message = Messages.Balance.NotFound
                    };
                    _errorOperations.AddError(error);
                    return NotFound(new ErrorDataResult<ErrorDTO>(error, Messages.Balance.NotFound, 404));
                }

                // Restore product stocks
                foreach (var preOrderProduct in preOrder.PreOrderProducts)
                {
                    preOrderProduct.Product.Stock += preOrderProduct.Quantity;
                }

                balance.BlockedBalance -= preOrder.Amount;
                balance.AvailableBalance += preOrder.Amount;
                balance.LastUpdated = DateTime.UtcNow;

                if (!_balanceOperations.UpdateBalance(balance))
                {
                    var error = new ErrorDTO
                    {
                        Name = "BalanceUpdateError",
                        Message = Messages.Balance.UpdateFailed
                    };
                    _errorOperations.AddError(error);
                    return StatusCode(500, new ErrorDataResult<ErrorDTO>(error, Messages.Balance.UpdateFailed, 500));
                }

                preOrder.Status = "Cancelled";
                preOrder.CancelledAt = DateTime.UtcNow;

                _dbContext.PreOrders.Update(preOrder);
                _dbContext.SaveChanges();

                var response = new
                {
                    OrderId = preOrder.Id,
                    CancelledAt = preOrder.CancelledAt?.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return Ok(new SuccessDataResult<object>(response, Messages.PreOrder.Cancelled));
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
