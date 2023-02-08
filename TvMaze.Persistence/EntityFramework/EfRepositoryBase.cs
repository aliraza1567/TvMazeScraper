namespace TvMaze.Persistence.EntityFramework
{
    public abstract class EfRepositoryBase
    {
        protected readonly IEfUnitOfWork UnitOfWork;

        protected EfRepositoryBase(IEfUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
