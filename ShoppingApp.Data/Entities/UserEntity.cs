using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Data.Enums;

namespace ShoppingApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
