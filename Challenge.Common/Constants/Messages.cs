namespace Challenge.Common.Constants;

public static class Messages
{
    /// <summary>
    /// Contains authentication-related messages.
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        /// Message indicating that the user ID was not found in the token.
        /// </summary>
        public const string UserIdNotFound = "User ID not found in token";

        /// <summary>
        /// Message indicating unauthorized access.
        /// </summary>
        public const string Unauthorized = "Unauthorized access";
    }

    /// <summary>
    /// Contains messages related to balance operations.
    /// </summary>
    public static class Balance
    {
        /// <summary>
        /// Message indicating that the balance was not found.
        /// </summary>
        public const string NotFound = "Balance not found.";

        /// <summary>
        /// Message indicating that the balance was successfully retrieved.
        /// </summary>
        public const string Retrieved = "Balance retrieved successfully.";

        /// <summary>
        /// Message indicating an invalid balance request.
        /// </summary>
        public const string ValidationError = "Invalid balance request.";

        /// <summary>
        /// Message indicating that the balance is insufficient.
        /// </summary>
        public const string InsufficientBalance = "Insufficient balance.";

        /// <summary>
        /// Message indicating that the balance update failed.
        /// </summary>
        public const string UpdateFailed = "Balance update failed.";
    }

    /// <summary>
    /// Contains messages related to pre-order operations.
    /// </summary>
    public static class PreOrder
    {
        /// <summary>
        /// Message indicating an invalid order ID.
        /// </summary>
        public const string InvalidOrderId = "Invalid order ID.";

        /// <summary>
        /// Message indicating that the pre-order was not found.
        /// </summary>
        public const string NotFound = "Pre-order not found.";

        /// <summary>
        /// Message indicating that the pre-order was successfully created.
        /// </summary>
        public const string Created = "Pre-order created successfully.";

        /// <summary>
        /// Message indicating that the order was completed successfully.
        /// </summary>
        public const string Completed = "Order completed successfully.";

        /// <summary>
        /// Message indicating that the order is already completed.
        /// </summary>
        public const string AlreadyCompleted = "Order is already completed.";

        /// <summary>
        /// Message indicating that an already completed order cannot be cancelled.
        /// </summary>
        public const string AlreadyCompletedCantCancel = "Cannot cancel an already completed order.";

        /// <summary>
        /// Message indicating that the order is already cancelled.
        /// </summary>
        public const string AlreadyCancelled = "Order is already cancelled.";

        /// <summary>
        /// Message indicating that the order was cancelled successfully.
        /// </summary>
        public const string Cancelled = "Order cancelled successfully.";

        /// <summary>
        /// Message format for indicating that a product with a specific ID was not found.
        /// </summary>
        public const string ProductNotFound = "Product with ID {0} not found";

        /// <summary>
        /// Message format for indicating insufficient stock for a product.
        /// </summary>
        public const string InsufficientStock = "Insufficient stock for product {0}. Available: {1}, Requested: {2}";
    }

    /// <summary>
    /// Contains messages related to product operations.
    /// </summary>
    public static class Product
    {
        /// <summary>
        /// Message indicating that the product was not found.
        /// </summary>
        public const string NotFound = "Product not found.";

        /// <summary>
        /// Message indicating that the product was retrieved successfully.
        /// </summary>
        public const string Retrieved = "Product retrieved successfully.";

        /// <summary>
        /// Message indicating an invalid product request.
        /// </summary>
        public const string ValidationError = "Invalid product request.";

        /// <summary>
        /// Message indicating that the product is out of stock.
        /// </summary>
        public const string OutOfStock = "Product is out of stock.";
    }

    /// <summary>
    /// Contains messages related to user operations.
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Message indicating that the user was not found.
        /// </summary>
        public const string NotFound = "User not found.";

        /// <summary>
        /// Message indicating an invalid login request.
        /// </summary>
        public const string ValidationError = "Invalid login request.";

        /// <summary>
        /// Message indicating invalid username or password.
        /// </summary>
        public const string InvalidCredentials = "Invalid username or password.";

        /// <summary>
        /// Message indicating a successful login.
        /// </summary>
        public const string LoginSuccess = "Login successful.";
    }

    /// <summary>
    /// Contains generic error messages.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// Message indicating that an unexpected error occurred.
        /// </summary>
        public const string UnexpectedError = "An unexpected error occurred.";
    }

}