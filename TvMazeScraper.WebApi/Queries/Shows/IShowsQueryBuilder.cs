using TvMaze.Domain.Models;

namespace TvMazeScraper.WebApi.Queries.Shows;

public interface IShowsQueryBuilder : IListRequestBuilder<Show, ListShowsQueryDto>
{

}