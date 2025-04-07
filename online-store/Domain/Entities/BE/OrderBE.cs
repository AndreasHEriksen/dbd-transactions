namespace online_store.Domain.Entities.BE;

public class OrderBE
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public AddressBE ShippingAddress { get; set; }
    public List<OrderItemBE> Items { get; set; } = new List<OrderItemBE>();
    public Guid? PaymentId { get; set; }
}

public class OrderItemBE
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class AddressBE
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}
