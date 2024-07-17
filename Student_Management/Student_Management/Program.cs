using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using DinkToPdf;
using DinkToPdf.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add DBContext
builder.Services.AddDbContext<StudentDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DbCS"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DinkToPdf config
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add Redis Cache
builder.Services.AddDistributedRedisCache(options =>
{
	options.Configuration = "localhost:6379";
	options.InstanceName = "redisOne";
});


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
