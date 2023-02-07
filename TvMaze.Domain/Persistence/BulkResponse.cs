namespace TvMaze.Domain.Persistence
{
    public abstract class BulkResponse<TEntity>
        where TEntity : class, IEntity
    {

        public BulkResponse(IEnumerable<TEntity> results)
        {
            Results = results ?? new List<TEntity>();
            ResultCount = Results?.Count() ?? 0;
        }


        public IEnumerable<TEntity> Results { get; }

        public bool Any() => Results.Any();
        public int ResultCount { get; set; }

    }

    public class EntityListResponse<TEntity> : BulkResponse<TEntity> where TEntity : class, IEntity
    {
        public static EntityListResponse<TEntity> Empty = new EntityListResponse<TEntity>(new List<TEntity>(), 0, 0);

        public bool OnlyCount { get; }

        public int TotalResultCount { get; }

        public int SkippedResultCount { get; set; }

        public EntityListResponse(IEnumerable<TEntity> results, int totalResultCount, int skippedResultCount) : base(results)
        {
            TotalResultCount = totalResultCount;
            SkippedResultCount = skippedResultCount;
            OnlyCount = false;
        }

        public EntityListResponse(int resultCount, int totalResultCount, int skippedResultCount) : base(new List<TEntity>())
        {
            ResultCount = resultCount;
            TotalResultCount = totalResultCount;
            SkippedResultCount = skippedResultCount;
            OnlyCount = true;
        }

    }

    public class EntityUpdateResponse<TEntity> : BulkResponse<TEntity> where TEntity : class, IEntity
    {
        public static EntityUpdateResponse<TEntity> Empty = new EntityUpdateResponse<TEntity>(new List<TEntity>());

        public EntityUpdateResponse(IEnumerable<TEntity> results) : base(results)
        {
        }

    }

    public class EntityDeleteResponse<TEntity> : BulkResponse<TEntity> where TEntity : class, IEntity
    {
        public static EntityDeleteResponse<TEntity> Empty = new EntityDeleteResponse<TEntity>(new List<TEntity>());


        public EntityDeleteResponse(IEnumerable<TEntity> results) : base(results)
        {
        }

    }

    public class EntityInsertResponse<TEntity> : BulkResponse<TEntity> where TEntity : class, IEntity
    {
        public static EntityInsertResponse<TEntity> Empty = new EntityInsertResponse<TEntity>(new List<TEntity>());

        public EntityInsertResponse(IEnumerable<TEntity> results) : base(results)
        {
        }
    }

}
