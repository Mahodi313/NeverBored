using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeverBored.Database;
using NeverBored.Models;
using NeverBored.Services;
using System.Data.SqlTypes;

namespace NeverBored.Pages.ActivityView
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        public List<ActivityModel> Activites { get; set; } = new();

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            ActivityRepository activityRepo = new(_dbContext);

            Activites = await activityRepo.GetAll();
        }
    }
}
