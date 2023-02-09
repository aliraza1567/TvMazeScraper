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
            var result = await _showsRepository.GetByShowIdAsync(showId, cancellationToken);

            if(result != null)
                result.Casts = result.Casts.OrderByDescending(cast => cast.Birthday).ToList();

            return result;
        }

        public async Task<IList<Show>> GetAllWithCastSortedAsync(EntityListRequest<Show> listRequest, CancellationToken cancellationToken)
        {
           var entityListResponse = await _showsRepository.ListAsync(listRequest, cancellationToken);
            var allShows = entityListResponse.Results.ToList();

            allShows.ForEach(show =>
            {
                show.Casts = show.Casts.OrderByDescending(cast => cast.Birthday).ToList();
            });

            return allShows;
        }

        public async Task<IList<Show>> GetAllShowAsync(CancellationToken cancellationToken)
        {
            var listRequest = new EntityListRequest<Show>(e => e.ShowId, SortDirectionEnum.Ascending);

            var entityListResponse = await _showsRepository.ListAsync(listRequest, cancellationToken);
            var allShows = entityListResponse.Results.ToList();

            return allShows;
        }

        public async Task<EntityInsertResponse<Show>> SaveShowsAsync(List<Show> allShows, CancellationToken cancellationToken)
        {
            var entityInsertRequest = new EntityInsertRequest<Show>
            {
                EntitiesToInsert = allShows
            };

            var response = await _showsRepository.BulkInsertAsync(entityInsertRequest, cancellationToken);
            
            return response;
        }
    }
}
