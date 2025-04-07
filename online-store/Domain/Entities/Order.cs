using online_store.Domain.Entities.BE;

namespace online_store.Domain.Entities;

public class Order
    {
        private readonly OrderBE _be;

        public Order(Guid customerId, Address shippingAddress)
        {
            _be = new OrderBE
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Status = "Pending",
                ShippingAddress = shippingAddress.GetBE(),
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItemBE>(),
                TotalAmount = 0
            };
        }

        // For loading from persistence
        public Order(OrderBE be)
        {
            _be = be;
        }

        // Properties that expose BE data
        public Guid Id => _be.Id;
        public Guid CustomerId => _be.CustomerId;
        public string Status => _be.Status;
        public decimal TotalAmount => _be.TotalAmount;
        public DateTime OrderDate => _be.OrderDate;
        
        public Address ShippingAddress => new Address(_be.ShippingAddress);
        
        public IReadOnlyList<OrderItem> Items => 
            _be.Items.Select(item => new OrderItem(item)).ToList();
            
        public Guid? PaymentId => _be.PaymentId;

        // Get underlying backend entity for persistence
        public OrderBE GetBE() => _be;

        // Domain methods
        public void AddOrderItem(OrderItem item)
        {
            _be.Items.Add(item.GetBE());
            RecalculateTotalAmount();
        }

        public void RemoveOrderItem(Guid productId)
        {
            var item = _be.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _be.Items.Remove(item);
                RecalculateTotalAmount();
            }
        }

        public void UpdateStatus(string newStatus)
        {
            _be.Status = newStatus;
        }

        public void AssignPayment(Payment payment)
        {
            _be.PaymentId = payment.Id;
            
            if (payment.Status == "Completed")
            {
                _be.Status = "Paid";
            }
        }

        private void RecalculateTotalAmount()
        {
            _be.TotalAmount = _be.Items.Sum(item => item.Price * item.Quantity);
        }
    }

    public class OrderItem
    {
        private readonly OrderItemBE _be;

        public OrderItem(Guid productId, string productName, decimal price, int quantity)
        {
            _be = new OrderItemBE
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };
        }

        // For loading from persistence
        public OrderItem(OrderItemBE be)
        {
            _be = be;
        }

        // Properties that expose BE data
        public Guid Id => _be.Id;
        public Guid OrderId => _be.OrderId;
        public Guid ProductId => _be.ProductId;
        public string ProductName => _be.ProductName;
        public decimal Price => _be.Price;
        public int Quantity => _be.Quantity;

        // Get underlying backend entity for persistence
        public OrderItemBE GetBE() => _be;
    }

    public class Address
    {
        private readonly AddressBE _be;

        public Address(string street, string city, string state, string country, string zipCode)
        {
            _be = new AddressBE
            {
                Street = street,
                City = city,
                State = state,
                Country = country,
                ZipCode = zipCode
            };
        }

        // For loading from persistence
        public Address(AddressBE be)
        {
            _be = be;
        }

        // Properties that expose BE data
        public string Street => _be.Street;
        public string City => _be.City;
        public string State => _be.State;
        public string Country => _be.Country;
        public string ZipCode => _be.ZipCode;

        // Get underlying backend entity for persistence
        public AddressBE GetBE() => _be;
    }
