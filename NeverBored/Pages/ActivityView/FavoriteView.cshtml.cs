using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeverBored.Database;
using NeverBored.Models;
using NeverBored.Utilities;

namespace NeverBored.Pages.ActivityView
{
    public class FavoriteViewModel : PageModel
    {
        private readonly CookieService _cookieService;
        private readonly AppDbContext _dbContext;

        public List<ActivityModel> FavoriteActivites { get; set; }

        public FavoriteViewModel(CookieService cookieService, AppDbContext dbContext)
        {
            _cookieService = cookieService;
            _dbContext = dbContext;

        }
        public void OnGet()
        {
            List<int> favoriteActivitiesId = _cookieService.GetFavoriteActivities();
            FavoriteActivites = _dbContext.Activites.Where(a => favoriteActivitiesId.Contains(a.Id)).ToList(); 
        }

        public IActionResult OnPost(int id) 
        {
            _cookieService.DeleteFavoriteActivity(id);

            return RedirectToPage();
        }
    }
}
