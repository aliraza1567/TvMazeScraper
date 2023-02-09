namespace TvMaze.Domain.Persistence
{
    public class PagingOptions
    {
        public static PagingOptions Empty = new PagingOptions();

        public PagingOptions()
        {
            SkipCount = 0;
            TakeCount = 0;
        }

        public int SkipCount { get; set; }

        public int TakeCount { get; set; }

        public bool HasPagingOptions => SkipCount > 0 || TakeCount > 0;
    }

}
