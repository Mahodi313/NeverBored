using NeverBored.Database;
using NeverBored.Models;
using NeverBored.Services;
using Newtonsoft.Json;

public class ApiCaller
{
    private readonly AppDbContext _dbContext;
    private readonly HttpClient _httpClient;

    public ApiCaller(AppDbContext dbContext, HttpClient httpClient)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.BaseAddress = new Uri("https://www.boredapi.com/");
    }

    public async Task<List<Root>> MakeMultipleCalls(int numberOfCalls)
    {
        List<Root> results = new List<Root>();

        try
        {
            for (int i = 0; i < numberOfCalls; i++)
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/activity/");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Root? result = JsonConvert.DeserializeObject<Root>(json);

                    if (result != null)
                    {
                        ActivityRepository activityRepo = new(_dbContext);
                        ActivityModel? existingActivity = await activityRepo.GetByName(result.Activity);

                        if (existingActivity == null)
                        {
                            ActivityModel newActivity = new ActivityModel
                            {
                                Name = result.Activity,
                                Type = result.Type,
                                Participants = result.Participants.GetValueOrDefault(),
                                Link = result.Link
                            };

                            _dbContext.Activites.Add(newActivity);
                            await activityRepo.SaveChanges();
                        }

                        results.Add(result);
                    }
                }
                else
                {
                    throw new HttpRequestException($"Failed to fetch activity data from URL: {_httpClient.BaseAddress}/api/activity/");
                }
            }

            return results;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while fetching activity data: {ex.Message}", ex);
        }
    }
}
