using TvMaze.Domain.Persistence;

namespace TvMazeScraper.WebApi.Queries;

public interface IListRequestBuilder<TEntity, in TListRequestQuery>
    where TEntity : class, IEntity, new()
    where TListRequestQuery : ListRequestQueryDto, new()
{
    EntityListRequest<TEntity> Build(TListRequestQuery listQuery);
}