using BBQ_Schedule.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBQ_Schedule.Infra.Data.Mapping
{
    internal class GuestMapping
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(g => g.WithDrink)
                .HasColumnName("With_Drink")
                .IsRequired();

            builder.Property(g => g.Contribution)
                .IsRequired()
                .HasColumnType("decimal(38, 10)");

            builder.ToTable("Guest");
        }
    }
}
