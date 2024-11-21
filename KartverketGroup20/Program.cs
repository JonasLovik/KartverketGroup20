using KartverketGroup20.APIModels;
using KartverketGroup20.Data;
using KartverketGroup20.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

//Bind the API settings from appsettings.json
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

//Register services and their interfaces
builder.Services.AddHttpClient<IKommuneInfoService, KommuneInfoService>();
builder.Services.AddHttpClient<IStedsnavnService, StedsnavnService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure Entity Framework with MariaDB
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
new MySqlServerVersion(new Version(10, 5, 9))));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;  
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied/";
});

builder.Services.AddTransient<IDbConnection>((sp) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new MySqlConnection(connectionString);
});

builder.Services.AddScoped<ReportService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Administrator", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@kartverket.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, "Admin@123");
        await userManager.AddToRoleAsync(adminUser, "Administrator");
    }
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
