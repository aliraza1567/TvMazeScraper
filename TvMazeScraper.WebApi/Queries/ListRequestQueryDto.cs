namespace TvMazeScraper.WebApi.Queries
{
    public class ListRequestQueryDto
    {
        public const int DefaultLimit = 100;
        public const int MaxLimit = 5000;
        private int? _skip;
        private int? _take;
        private bool _takeValueSet;
        public ListRequestQueryDto()
        {
            OnlyCount = false;
            _skip = 0;
            _take = DefaultLimit;
        }

        public bool OnlyCount { get; set; }
        public int? Skip
        {
            get => _skip;
            set
            {
                _skip = value > 0 ? value : 0;
                if (!_takeValueSet)
                {
                    _take = 0;
                }
            }
        }

        public int? Take
        {
            get => _take;
            set
            {
                _take = value > 0 ? value <= MaxLimit ? value : MaxLimit : DefaultLimit;
                _takeValueSet = true;
            }
        }

    }
}
