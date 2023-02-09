namespace TvMazeScraper.WebApi.Integration.Tests.Infrastructure
{
    internal class TvMazeMockHttpMessageHandler : MockHttpMessageHandler
    {
        public TvMazeMockHttpMessageHandler()
        {

            this.When(HttpMethod.Get, "https://api.tvmaze.com/shows*")
                .Respond(System.Net.HttpStatusCode.OK);

            this.When(HttpMethod.Get, "https://api.tvmaze.com/shows/82/cast*")
                .Respond(System.Net.HttpStatusCode.OK);
        }
    }
}
