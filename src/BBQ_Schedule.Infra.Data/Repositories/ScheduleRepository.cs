using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Interfaces.Repositories;
using BBQ_Schedule.Domain.Models;
using BBQ_Schedule.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BBQ_Schedule.Infra.Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ScheduleContext Db;
        public ScheduleRepository(ScheduleContext db)
        {
            Db = db;
        }

        public IUnitOfWork UnitOfWork => Db;

        public void AddEvent(Schedule schedule)
        {
            Db.Schedules.Add(schedule);
        }

        public void AddGuest(Guest guest)
        {
            Db.Guests.Add(guest);
        }

        public void UpdateEvent(Schedule schedule)
        {
            Db.Schedules.Update(schedule);
        }
        public void Dispose()
        {
            Db?.Dispose();
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await Db.Schedules.ToListAsync();
        }

        public async Task<Schedule> GetAsync(Guid id)
        {
            return await Db.Schedules.FindAsync(id);
        }

        public async Task<Schedule> GetEventByDateAsync(DateOnly date)
        {
            return await Db.Schedules.FirstOrDefaultAsync(s => s.Date.Equals(date));
        }

        public async Task<Schedule> GetEventByIdAsync(Guid id)
        {
            return await Db.Schedules.FindAsync(id);
                
        }

        public async Task<Schedule> GetEventWitGuestsByIdAsync(Guid id)
        {
            return await Db.Schedules.Include(g => g.Guests).FirstOrDefaultAsync(s => s.Id.Equals(id));
        }
        public async Task<List<Schedule>> GetEventsAsync()
        {
            return await Db.Schedules.ToListAsync();
        }
        public void RemoveEvent(Schedule schedule)
        {
            Db.Schedules.Remove(schedule);
        }
    }
}
