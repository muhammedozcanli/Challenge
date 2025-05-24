namespace Challenge.Common.Constants;

public static class Messages
{
    public static class Balance
    {
        public const string NotFound = "Balance not found";
        public const string Retrieved = "Balance retrieved successfully";
        public const string InsufficientBalance = "Insufficient available balance for pre-order";
        public const string UpdateFailed = "Failed to update balance";
        public const string ValidationError = "Required fields are missing or invalid";
    }

    public static class PreOrder
    {
        public const string Created = "Pre-order created successfully";
        public const string NotFound = "PreOrder not found with the specified OrderId";
        public const string AlreadyCompleted = "This order has already been completed";
        public const string AlreadyCompletedCantCancel = "This order has already been completed and cannot be cancelled";
        public const string AlreadyCancelled = "This order has already been cancelled";
        public const string Completed = "Order completed successfully";
        public const string Cancelled = "Order cancelled successfully";
        public const string InvalidOrderId = "OrderId is required and must be valid";
    }

    public static class Product
    {
        public const string NotFound = "No products found";
        public const string Retrieved = "Products retrieved successfully";
    }

    public static class Error
    {
        public const string UnexpectedError = "An unexpected error occurred";
    }
} 