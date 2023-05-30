using BBQ_Schedule.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBQ_Schedule.Infra.Data.Mapping
{
    internal class ScheduleMapping
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Date)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(s => s.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(s => s.Location)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(s => s.Capacity)
                .IsRequired();

            builder.Property(s => s.TotalPeople)
                .IsRequired()
                .HasColumnName("Total_People");

            builder.Property(s => s.TotalCollected)
                .IsRequired()
                .HasColumnName("Total_Collected")
                .HasColumnType("decimal(38, 10)");

            builder.HasMany(s => s.Guests)
                .WithOne(g => g.Schedule)
                .HasForeignKey(g => g.ScheduleId);

            builder.ToTable("Schedule");
        }
    }
}
