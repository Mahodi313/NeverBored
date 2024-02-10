using NeverBored.Database;
using NeverBored.Models;
using NeverBored.Services;
using Newtonsoft.Json;

namespace NeverBored.Api
{
    public class ApiCaller
    {
        private readonly AppDbContext _dbcontext;
        public HttpClient Client { get; set; }

        public ApiCaller(AppDbContext dbContext)
        {
            _dbcontext = dbContext;

            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://www.boredapi.com/");
        }

        public async Task<Root> MakeCall()
        {
            try
            {
                string url = "api/activity/";
                ActivityRepository activityRepo = new(_dbcontext);

                HttpResponseMessage response = await Client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Root? result = JsonConvert.DeserializeObject<Root>(json);

                    if (result != null)
                    {
                        ActivityModel? existingActivity = await activityRepo.GetByName(result.Activity);

                        if (existingActivity == null)
                        {
                            ActivityModel newActivity = new()
                            {
                                Name = result.Activity,
                                Type = result.Type,
                                Participants = result.Participants.GetValueOrDefault(),
                                Link = result.Link
                            };

                            await activityRepo.Add(newActivity);
                        }

                        await activityRepo.SaveChanges();

                        return result;
                    }
                }
                throw new HttpRequestException($"Failed to fetch activity data from URL: {url}");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"An error occured while fetching activity data: {ex.Message}");
            }
        }
    }
}
