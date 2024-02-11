using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeverBored.Database;
using NeverBored.Models;
using NeverBored.Services;
using NeverBored.Utilities;
using System.Data.SqlTypes;

namespace NeverBored.Pages.ActivityView
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;
        private readonly CookieService _cookieService;

        public List<ActivityModel> Activites { get; set; } = new();

        public IndexModel(AppDbContext dbContext, CookieService cookieService)
        {
            _cookieService = cookieService;
            _dbContext = dbContext;

        }

        public async Task OnGet()
        {
            ActivityRepository activityRepo = new(_dbContext);

            Activites = await activityRepo.GetAll();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            _cookieService.AddFavoriteActivity(id);
            return RedirectToPage();
        }
    }
}
