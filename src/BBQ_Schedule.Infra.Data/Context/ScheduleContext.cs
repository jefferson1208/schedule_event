using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BBQ_Schedule.Infra.Data.Context
{
    public class ScheduleContext : DbContext, IUnitOfWork
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options):base(options) 
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
