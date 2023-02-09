using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TvMaze.External.Clients;
using TvMaze.Persistence.EntityFramework;

namespace TvMazeScraper.WebApi.Integration.Tests.Infrastructure
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        public CustomWebApplicationFactory()
        {
            this.Server.AllowSynchronousIO = true;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Database
                var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<TvMazeDbContext>));
                services.Remove(descriptor);
                services.AddDbContext<TvMazeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });


                //HttpClients
                var httpClientServiceDescriptors = services.First(d => d.ServiceType == typeof(IHttpClientFactory));
                services.Remove(httpClientServiceDescriptors);
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                var mockHttpMessageHandler = new TvMazeMockHttpMessageHandler();

                httpClientFactoryMock.Setup(x => x.CreateClient(It.Is<string>(s => s == TvMazeClient.ClientName)))
                    .Returns(new HttpClient(mockHttpMessageHandler));
                services.AddSingleton(httpClientFactoryMock.Object);

                //Initializations
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TvMazeDbContext>();
                db.Database.EnsureCreated();
                DbUtilities.ReInitializeDbForTests(db);
            });
        }

        internal HttpClient CreateHttpClient()
        {
            var client = CreateClient();
            return client;
        }

        
    }


}
