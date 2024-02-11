using Microsoft.EntityFrameworkCore;
using NeverBored.Database;
using NeverBored.Services;
using NeverBored.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IActivity, ActivityRepository>();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddScoped<ApiCaller>();

builder.Services.AddScoped<CookieService>();

var app = builder.Build();

InitializeDatabase(app.Services).Wait();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

static async Task InitializeDatabase(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var apiCaller = serviceProvider.GetRequiredService<ApiCaller>();

        if (!dbContext.Activites.Any())
        {
            await apiCaller.MakeMultipleCalls(20);
        }
    }
}