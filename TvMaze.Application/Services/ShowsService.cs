using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMaze.Persistence.Abstractions.Repositories;

namespace TvMaze.Application.Services
{
    public class ShowsService: IShowsService
    {
        private readonly IShowsRepository _showsRepository;

        public ShowsService(IShowsRepository showsRepository)
        {
            _showsRepository = showsRepository;
        }

        public async Task<Show> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var show = await _showsRepository.GetAsync(id, cancellationToken);
            return show;
        }

        public async Task<Show> GetByShowIdAsync(int showId, CancellationToken cancellationToken)
        {
            var result = await _showsRepository.SingleOrDefaultAsync(show => show.ShowId == showId, cancellationToken);

            result.Casts = result.Casts.OrderByDescending(cast => cast.Birthday).ToList();

            return result;
        }

        public async Task<IList<Show>> GetAll(CancellationToken cancellationToken)
        {
            var findRequest = new EntityListRequest<Show>
            {
                Sorting = new SortingOptions<Show>
                {
                    SortItems = new List<SortItem<Show>>
                    {
                        new()
                        {
                            SortField = show => show.ShowId,
                            SortDirection = SortDirectionEnum.Descending
                        }
                    }
                }
            };

            var entityListResponse = await _showsRepository.ListAsync(findRequest, cancellationToken);
            var allShows = entityListResponse.Results.ToList();

            allShows.ForEach(show =>
            {
                show.Casts = show.Casts.OrderByDescending(cast => cast.Birthday).ToList();
            });

            return allShows;
        }
    }
}
