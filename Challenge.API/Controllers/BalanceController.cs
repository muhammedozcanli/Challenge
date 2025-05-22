using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Persistence.DTOs;
using Challenge.Persistence.Entities;
using Challenge.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        IBalanceOperations _balanceOperations;
        IErrorOperations _errorOperations;
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
                var balance = _balanceOperations.GetBalances().FirstOrDefault();

                if (balance is null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "NotFound",
                        Message = "Balance not found"
                    };

                    _errorOperations.AddError(error);

                    return ResponseHelper.Error(404, error.Name, error.Message);
                }

                var data = new
                {
                    userId = balance.UserId,
                    totalBalance = balance.AvailableBalance + balance.BlockedBalance,
                    availableBalance = balance.AvailableBalance,
                    blockedBalance = balance.BlockedBalance,
                    currency = balance.Currency,
                    lastUpdated = balance.LastUpdated
                };

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


        [HttpPost("preorder")]
        [SwaggerOperation(Summary = "Create a pre-order", Description = "Creates a pre-order and blocks the specified amount from the available balance")]
        public IActionResult CreatePreOrder([FromBody] PreOrderDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ResponseHelper.Error(
                        400,
                        "ValidationError",
                        "Required fields are missing or invalid."
                    );
                }

                var balance = _balanceOperations.GetBalances().FirstOrDefault();

                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "NotFound",
                        Message = "Balance not found"
                    };

                    _errorOperations.AddError(error);

                    return ResponseHelper.Error(404, error.Name, error.Message);
                }

                if (balance.AvailableBalance < request.Amount)
                {
                    return ResponseHelper.Error(
                        400,
                        "InsufficientBalance",
                        "Insufficient available balance for pre-order."
                    );
                }

                balance.BlockedBalance += request.Amount;
                balance.AvailableBalance -= request.Amount;

                var updateResult = _balanceOperations.UpdateBalance(balance);

                if (!updateResult)
                {
                    return ResponseHelper.Error(
                        500,
                        "UpdateFailed",
                        "Failed to update balance."
                    );
                }

                var preOrder = new PreOrder
                {
                    OrderId = request.OrderId,
                    Amount = request.Amount,
                    Status = "PreOrder created!",
                    CreatedDate = DateTime.UtcNow,
                };

                _dbContext.PreOrders.Add(preOrder);
                _dbContext.SaveChanges();

                return ResponseHelper.Success(new
                {
                    orderId = request.OrderId,
                    blockedAmount = request.Amount
                });
            }
            catch (Exception ex)
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

        [HttpPost("complete")]
        [SwaggerOperation(Summary = "Create an order", Description = "Completes a pre-order by deducting the blocked amount from the total balance")]
        public IActionResult CompleteOrder([FromBody] Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                {
                    return ResponseHelper.Error(
                        400,
                        "ValidationError",
                        "OrderId is required and must be valid."
                    );
                }

                var preOrder = _dbContext.PreOrders.FirstOrDefault(po => po.OrderId == orderId);

                if (preOrder == null)
                {
                    return ResponseHelper.Error(
                        404,
                        "NotFound",
                        "PreOrder not found with the specified OrderId."
                    );
                }

                if (preOrder.CompletedAt != null)
                {
                    return ResponseHelper.Error(
                        400,
                        "AlreadyCompleted",
                        "This order has already been completed."
                    );
                }

                preOrder.Status = "Completed";
                preOrder.CompletedAt = DateTime.UtcNow;

                _dbContext.PreOrders.Update(preOrder);
                _dbContext.SaveChanges();

                return ResponseHelper.Success(new
                {
                    orderId = preOrder.OrderId,
                    status = preOrder.Status,
                    completedAt = preOrder.CompletedAt
                });
            }
            catch (Exception ex)
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

        [HttpPost("cancel")]
        [SwaggerOperation(Summary = "Cancel a pre-order", Description = "Cancels a pre-order and returns the blocked amount to the available balance")]
        public IActionResult CancelOrder([FromBody] Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                {
                    return ResponseHelper.Error(
                        400,
                        "ValidationError",
                        "OrderId is required and must be valid."
                    );
                }

                var preOrder = _dbContext.PreOrders.FirstOrDefault(po => po.OrderId == orderId);

                if (preOrder == null)
                {
                    return ResponseHelper.Error(
                        404,
                        "NotFound",
                        "PreOrder not found with the specified OrderId."
                    );
                }

                if (preOrder.CompletedAt != null)
                {
                    return ResponseHelper.Error(
                        400,
                        "AlreadyCompleted",
                        "This order has already been completed and cannot be cancelled."
                    );
                }

                if (preOrder.CancelledAt != null)
                {
                    return ResponseHelper.Error(
                        400,
                        "AlreadyCancelled",
                        "This order has already been cancelled."
                    );
                }

                var balance = _balanceOperations.GetBalances().FirstOrDefault();

                if (balance == null)
                {
                    var error = new ErrorDTO
                    {
                        Name = "NotFound",
                        Message = "Balance not found"
                    };

                    _errorOperations.AddError(error);

                    return ResponseHelper.Error(404, error.Name, error.Message);
                }

                balance.BlockedBalance -= preOrder.Amount;
                balance.AvailableBalance += preOrder.Amount;

                var updateResult = _balanceOperations.UpdateBalance(balance);

                if (!updateResult)
                {
                    return ResponseHelper.Error(
                        500,
                        "UpdateFailed",
                        "Failed to update balance."
                    );
                }

                preOrder.Status = "Cancelled";
                preOrder.CancelledAt = DateTime.UtcNow;

                _dbContext.PreOrders.Update(preOrder);
                _dbContext.SaveChanges();

                return ResponseHelper.Success(new
                {
                    orderId = preOrder.OrderId,
                    status = preOrder.Status,
                    cancelledAt = preOrder.CancelledAt
                });
            }
            catch (Exception ex)
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
}
