using online_store.Domain.Entities.BE;

namespace online_store.Domain.Entities;

public class Product
{
    private readonly ProductBE _be;

    public Product(string name, string description, decimal price, int stockQuantity, string imageUrl)
    {
        _be = new ProductBE
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stockQuantity,
            ImageUrl = imageUrl,
            IsAvailable = stockQuantity > 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    // For loading from persistence
    public Product(ProductBE be)
    {
        _be = be;
    }

    // Properties that expose BE data
    public Guid Id => _be.Id;
    public string Name => _be.Name;
    public string Description => _be.Description;
    public decimal Price => _be.Price;
    public int StockQuantity => _be.StockQuantity;
    public string ImageUrl => _be.ImageUrl;
    public bool IsAvailable => _be.IsAvailable;
    public DateTime CreatedAt => _be.CreatedAt;
    public DateTime? UpdatedAt => _be.UpdatedAt;

    // Get underlying backend entity for persistence
    public ProductBE GetBE() => _be;

    // Domain methods
    public void UpdateStock(int quantity)
    {
        _be.StockQuantity = quantity;
        _be.IsAvailable = quantity > 0;
        _be.UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string description, decimal price, string imageUrl)
    {
        _be.Name = name;
        _be.Description = description;
        _be.Price = price;
        _be.ImageUrl = imageUrl;
        _be.UpdatedAt = DateTime.UtcNow;
    }
}
