namespace Challenge.Common.Constants;

public static class Messages
{
    public static class Balance
    {
        public const string NotFound = "Balance not found.";
        public const string Retrieved = "Balance retrieved successfully.";
        public const string ValidationError = "Invalid balance request.";
        public const string InsufficientBalance = "Insufficient balance.";
        public const string UpdateFailed = "Balance update failed.";
    }

    public static class PreOrder
    {
        public const string InvalidOrderId = "Invalid order ID.";
        public const string NotFound = "Pre-order not found.";
        public const string Created = "Pre-order created successfully.";
        public const string Completed = "Order completed successfully.";
        public const string AlreadyCompleted = "Order is already completed.";
        public const string AlreadyCompletedCantCancel = "Cannot cancel an already completed order.";
        public const string AlreadyCancelled = "Order is already cancelled.";
        public const string Cancelled = "Order cancelled successfully.";
    }

    public static class Product
    {
        public const string NotFound = "Product not found.";
        public const string Retrieved = "Product retrieved successfully.";
        public const string ValidationError = "Invalid product request.";
        public const string OutOfStock = "Product is out of stock.";
    }

    public static class User
    {
        public const string NotFound = "User not found.";
        public const string ValidationError = "Invalid login request.";
        public const string InvalidCredentials = "Invalid username or password.";
        public const string LoginSuccess = "Login successful.";
    }

    public static class Error
    {
        public const string UnexpectedError = "An unexpected error occurred.";
    }
} 