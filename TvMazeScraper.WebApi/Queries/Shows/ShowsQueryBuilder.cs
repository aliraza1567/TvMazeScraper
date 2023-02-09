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

            for (var index = 0; index < listQuery.SortFields.Count; index++)
            {
                var sortField = listQuery.SortFields[index];
                var sortDirectionDto = listQuery.SortDirections.ElementAtOrDefault(index);
                var sortDirection = _mapper.Map<SortDirectionEnum>(sortDirectionDto);

                //ShowId
                if (sortField is nameof(Show.ShowId))
                {
                    listRequest.Sorting.SortItems.Add(new SortItem<Show>
                    {
                        SortField = entity => entity.ShowId,
                        SortDirection = sortDirection
                    });
                }
            }

            return listRequest;
        }
    }
}
