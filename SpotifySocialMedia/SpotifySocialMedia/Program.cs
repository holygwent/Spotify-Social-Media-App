using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Services;
using SpotifySocialMedia.SpotifySettingsDatabase;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoSettingsDatabase"));
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<ISpotifyTokenService, SpotifyTokenService>();
builder.Services.AddScoped<ISearchService , SearchService>();
//mongo services
builder.Services.AddSingleton<IDatabaseAuthorizationCodeService, DatabaseAuthorizationCodeService>();
builder.Services.AddSingleton<IDatabaseSpotifyTokenService, DatabaseSpotifyTokenService>();

//http clients
builder.Services.AddHttpClient<ISpotifyTokenService, SpotifyTokenService>(c =>
{
    c.BaseAddress = new Uri("https://accounts.spotify.com");
    //  c.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
});
builder.Services.AddHttpClient<ISearchService, SearchService>(c =>
{
    c.BaseAddress = new Uri("https://api.spotify.com/v1/search");
    c.DefaultRequestHeaders.Add("Accept", "application/.json");
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.MapRazorPages();

app.Run();
