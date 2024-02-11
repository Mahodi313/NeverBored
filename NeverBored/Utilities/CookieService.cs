
namespace NeverBored.Utilities
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public void AddFavoriteActivity(int activityId)
        {
            var exisitingFavorites = GetFavoriteActivities();
            exisitingFavorites.Add(activityId);
            SaveFavoriteActivities(exisitingFavorites);
        }

        public List<int> GetFavoriteActivities()
        {
            var favoritesCookie = _contextAccessor.HttpContext.Request.Cookies["favoriteActivities"];
            if (!string.IsNullOrEmpty(favoritesCookie))
            {
                return favoritesCookie.Split(',').Select(int.Parse).ToList();
            }

            return new List<int>();
        }

        public void DeleteFavoriteActivity(in int activityId)
        {
            var existingFavorites = GetFavoriteActivities();
            if (existingFavorites.Any())
            {
                existingFavorites.Remove(activityId);
            }
            SaveFavoriteActivities(existingFavorites);
        }

        private void SaveFavoriteActivities(List<int> favoriteActivities)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append("favoriteActivities", string.Join(",", favoriteActivities));
        }
    }
}
