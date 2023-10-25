using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var con = builder.Configuration.GetConnectionString("SchoolManagementDbConnetion");
builder.Services.AddDbContext<SchoolManagementDbContext>(q=>q.UseSqlServer(con));
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// dotnet ef dbcontext scaffold "Server=localhost, 1433; Database=SchoolManagementDb;Trusted_Connection=false;MultipleActiveResultSets=false;Encrypt=false;user id=sa;password=123456a@" Microsoft.EntityFrameworkCore.SqlServer -o Data -f --no-onconfiguring

// dotnet aspnet-codegenerator controller -name EnrollmentsController -m Enrollment -dc SchoolManagementDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries -f 