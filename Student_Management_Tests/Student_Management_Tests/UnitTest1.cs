using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Student_Management;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class APIControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public APIControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<StudentDbContext>));
                services.Remove(descriptor);

                services.AddDbContext<StudentDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<StudentDbContext>();
                    SeedData.SeedingData(context);
                }
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetAllStudents_ReturnsListOfStudents()
    {
        var response = await _client.GetAsync("/API/GetAllStudents");

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var students = JsonConvert.DeserializeObject<List<Student>>(responseString);
        Assert.Equal(2, students.Count);
    }

    // Bạn có thể thêm các bài kiểm thử khác tương tự cho các API khác
}
