namespace TvMaze.Application.Abstractions.Contracts.Shows
{
    public class ShowsFilter
    {
        public static ShowsFilter Empty = new ShowsFilter();
        public int ShowId { get; set; }
        public string ShowName { get; set; }
    }
}
