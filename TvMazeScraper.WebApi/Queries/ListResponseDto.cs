namespace TvMazeScraper.WebApi.Queries
{
    public class ListResponseDto<TResource>
        where TResource : IResource<int>
    {
        public ListResponseDto()
        {
            Results = new List<TResource>();
        }

        public bool OnlyCount { get; set; }

        public int ResultCount { get; set; }

        public int TotalResultCount { get; set; }

        public int SkippedResultCount { get; set; }

        public IEnumerable<TResource> Results { get; }

    }
}
