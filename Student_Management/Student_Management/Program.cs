using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add DBContext
builder.Services.AddDbContext<StudentDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DbCS"));
});

// Add services to the container.
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

//Seeding Data
var context = app.Services.CreateScope().ServiceProvider.GetService<StudentDbContext>();
SeedData.SeedingData(context);

app.Run();
