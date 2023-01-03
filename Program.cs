using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

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
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
