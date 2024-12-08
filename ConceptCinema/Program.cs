using Constants.Authentification;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Authentification;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

var connectionStringUsers = builder.Configuration.GetConnectionString("DefaultConnectionUsers");
var connectionStringData = builder.Configuration.GetConnectionString("DefaultConnectionData");

builder.Services.AddDbContext<CinemaDbContext>(
    options => options.UseSqlServer(connectionStringData, b => b.MigrationsAssembly("ConceptCinema")));

builder.Services.AddDbContext<UsersDbContext>(
    options => options.UseSqlServer(connectionStringUsers, b => b.MigrationsAssembly("ConceptCinema")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<UsersDbContext>()

    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { UserRoles.Admin, UserRoles.DeliveryEmployee, UserRoles.User };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        var newAdmin = new AppUser
        {
            Name = "admin",
            Email = "admin@admin.com",
            UserName = "admin",
            Address = "admin",
        };
        var result = await userManager.CreateAsync(newAdmin, "123456789");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();