using AutoMapper;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;

namespace TvMazeScraper.WebApi.Queries.Shows
{
    public class ShowsQueryBuilder: IShowsQueryBuilder
    {
        private readonly IMapper _mapper;

        public ShowsQueryBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }
        public EntityListRequest<Show> Build(ListShowsQueryDto listQuery)
        {
            //Paging
            var listRequest = new EntityListRequest<Show>(e => e.Id, SortDirectionEnum.Ascending)
            {
                OnlyCount = listQuery.OnlyCount,
                Paging = new PagingOptions
                {
                    SkipCount = listQuery.Skip ?? -1,
                    TakeCount = listQuery.Take ?? -1
                }
            };

            return listRequest;
        }
    }
}
