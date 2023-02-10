namespace TvMazeScraper.WebApi.Queries
{
    public interface IResource
    {
    }

    public interface IResource<TKey> : IResource
    {
        public TKey ShowId { get; set; }
    }
}
