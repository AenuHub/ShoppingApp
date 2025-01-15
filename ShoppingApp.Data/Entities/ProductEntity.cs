using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingApp.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
