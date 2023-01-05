using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmpManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<ICRUD,CRUDRepository>();

builder.Services.AddScoped<ICRUD, DBCrud>();  // to work with db
builder.Services.AddScoped<IFileUpload, FileUpload>();

//SQLlite
//builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlite("Data Source=CCAD9Employees_lite.db"));

//connection to full SQL database
builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlServer("Server=localhost;Database=CCAD9Demo;Trusted_Connection=true;TrustServerCertificate=True; MultipleActiveResultSets=True"));
builder.Services.AddIdentity<User, IdentityRole>(Options =>
{
    Options.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<UserContext>();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer("Server=localhost;Database=CCAD9DemoUsers;TrustServerCertificate=True; Trusted_Connection=true;MultipleActiveResultSets=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope=app.Services.CreateScope())
{
    var dbcontext=scope.ServiceProvider.GetRequiredService<EmployeeContext>();
    dbcontext.Database.EnsureCreated();  //create the db
    var userdbcontext=scope.ServiceProvider.GetRequiredService<UserContext>();
    userdbcontext.Database.EnsureCreated();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //This has to go before Authorization or your program will catch fire!!

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
