using Microsoft.EntityFrameworkCore;
using NeverBored.Database;
using NeverBored.Models;

namespace NeverBored.Services
{
    public class ActivityRepository : IActivity
    {
        private readonly AppDbContext _dbContext;
        public ActivityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ActivityModel>> GetAll()
        {
            return await _dbContext.Activites.ToListAsync();
        }

        public async Task<ActivityModel?> GetByName(string name)
        {
            var activity = await _dbContext.Activites.FirstOrDefaultAsync(a => a.Name == name);

            if (activity != null)
            {
                return activity;
            }

            return null;
        }

        public async Task Add(ActivityModel activity)
        {
            await _dbContext.Activites.AddAsync(activity);
        }

        public void Delete(string name)
        {
            var activity = GetByName(name);

            if (activity != null)
            {
                _dbContext.Remove(activity);
            }
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
