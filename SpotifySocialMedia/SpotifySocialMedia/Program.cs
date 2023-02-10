using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SpotifySocialMedia.Areas.Admin.Services;
using SpotifySocialMedia.Data;
using SpotifySocialMedia.Hubs;
using SpotifySocialMedia.Services.Repositories;
using SpotifySocialMedia.Services.Repositories.Interfaces;
using SpotifySocialMedia.SpotifySettingsDatabase;
using SpotifySocialMedia.SpotifySettingsDatabase.Services;
using System.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using SpotifySocialMedia.Models;
using SpotifySocialMedia.Services;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
//sql connection
builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoSettingsDatabase"));
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<ISpotifyTokenService, SpotifyTokenService>();
builder.Services.AddScoped<ISearchService , SearchService>();
builder.Services.AddScoped<ISongRepository , SongRepository>();
builder.Services.AddScoped<ICommentRepository , CommentRepository>();
builder.Services.AddScoped<IRateRepository , RateRepository>();
builder.Services.AddScoped<IArtistRepository , ArtistRepository>();
builder.Services.AddScoped<IUserInformationService, UserInformationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
//mongo services
builder.Services.AddSingleton<IDatabaseAuthorizationCodeService, DatabaseAuthorizationCodeService>();
builder.Services.AddSingleton<IDatabaseSpotifyTokenService, DatabaseSpotifyTokenService>();
//sendgrid
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
//authentication external providers
builder.Services.AddAuthentication().AddFacebook(options =>
{
    
    options.ClientId = builder.Configuration["FacebookClientId"];
    options.ClientSecret = builder.Configuration["FacebookClientSecret"];
});
builder.Services.AddAuthentication().AddSpotify(options =>
{
    options.ClientId = builder.Configuration["Spotify:ClientId"];
    options.ClientSecret = builder.Configuration["Spotify:ClientSecret"];
    options.CallbackPath = "/Home";
    options.Scope.Add("user-read-email user-read-private");
    //options.SaveTokens = true;
   
   

});
//http clients
builder.Services.AddHttpClient<ISpotifyTokenService, SpotifyTokenService>(c =>
{
    c.BaseAddress = new Uri("https://accounts.spotify.com");
    //  c.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
});
builder.Services.AddHttpClient<ISearchService, SearchService>(c =>
{
    c.BaseAddress = new Uri("https://api.spotify.com/v1/");
    c.DefaultRequestHeaders.Add("Accept", "application/.json");
    
});
builder.Services.AddHttpClient<IArtistRepository, ArtistRepository>(c =>
{
    c.BaseAddress = new Uri("https://api.spotify.com/v1/");
    c.DefaultRequestHeaders.Add("Accept", "application/.json");

});
builder.Services.AddSignalR();
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
app.MapHub<CommentHub>("/Chat/Index");
app.Run();
