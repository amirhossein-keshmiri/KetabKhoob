using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //Agg Root
        builder.ToTable("Orders", "order");

        //Child Agg - One to Many
        builder.OwnsMany(b => b.Items, option =>
        {
            option.ToTable("Items", "order");
            //option.HasKey(b => b.Id);
            //option.HasIndex(b => b.InventoryId);
            //option.HasIndex(b => b.OrderId);
        });

        //Child Agg - One to One
        builder.OwnsOne(b => b.Address, option =>
        {
            option.HasKey(b => b.Id);
            option.ToTable("Addresses", "order");

            option.Property(b => b.State)
                .IsRequired().HasMaxLength(100);

            option.Property(b => b.City)
                .IsRequired().HasMaxLength(100);

            option.Property(b => b.Name)
                .IsRequired().HasMaxLength(50);

            option.Property(b => b.Family)
                .IsRequired().HasMaxLength(50);

            option.Property(b => b.PhoneNumber)
                .IsRequired().HasMaxLength(12);

            option.Property(b => b.NationalCode)
                .IsRequired().HasMaxLength(10);

            option.Property(b => b.PostalCode)
                .IsRequired().HasMaxLength(20);
        });

        //Value Object - Must Not Have Id
        builder.OwnsOne(b => b.Discount, option =>
        {
            option.Property(b => b.DiscountTitle)
                .IsRequired()
                .HasMaxLength(100);
        });

        //Value Object - Must Not Have Id
        builder.OwnsOne(b => b.ShippingMethod, option =>
        {
            option.Property(b => b.ShippingType)
                .IsRequired(false)
                .HasMaxLength(100);
        });
    }
}

