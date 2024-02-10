using NeverBored.Models;

namespace NeverBored.Services
{
    public interface IActivity
    {
        Task<ActivityModel?> GetByName(string name);
        Task<List<ActivityModel>> GetAll();
        void Delete(string name);
        Task Add(ActivityModel activity);
    }
}
