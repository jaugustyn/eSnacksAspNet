namespace eSnacks.Models.Enums;

public enum OrderStatus
{
    Pending = 1,
    AwaitingPayment = 2,
    AwaitingFulfillment = 3,
    AwaitingShipment = 4,
    Completed = 5,
    Shipped = 6,
    Cancelled = 7,
    Declined = 8,
    Refunded = 9,
    PartiallyRefunded = 10,
}