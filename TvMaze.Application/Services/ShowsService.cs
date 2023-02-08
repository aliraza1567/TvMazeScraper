using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Net.Sockets;
using TvMaze.Application.Abstractions.Contracts.Cast;
using TvMaze.Application.Abstractions.Contracts.Shows;
using TvMaze.Application.Abstractions.Services.Shows;
using TvMaze.Domain.Models;
using TvMaze.Domain.Persistence;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Application.Extensions;

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
            return result;
        }

        public async Task<IList<Show>> GetAll(CancellationToken cancellationToken)
        {
            var findRequest = new EntityListRequest<Show>(show => show.ShowId, SortDirectionEnum.Descending);

            var entityListResponse = await _showsRepository.ListAsync(findRequest, cancellationToken);
            return entityListResponse.Results.ToList();
        }


        #region Private Methods
        public Expression<Func<Show, bool>> BuildFilter(ShowsFilter filter)
        {
            Expression<Func<Show, bool>> expression = null;
            if (filter.ShowName != null)
            {
                expression = expression.AndAlso(x => x.Name == filter.ShowName);
            }
            if (filter.ShowId != null)
            {
                expression = expression.AndAlso(x => x.ShowId == filter.ShowId);
            }

            return expression ?? (entity => true);
        }
        private SortingOptions<Show> BuildSort(ShowsSort sort)
        {
            SortingOptions<Show> sortingOptions = new SortingOptions<Show>();

            for (int i = 0; i < sort.SortFields.Count; i++)
            {
                var sortField = sort.SortFields[i];
                var sortDirection = sort.SortDirections.ElementAtOrDefault(i);

                switch (sortField)
                {
                    case ShowsSortFieldEnum.ShowId:
                        sortingOptions.SortItems.Add(new SortItem<Show>
                        {
                            SortField = e => e.ShowId,
                            SortDirection = sortDirection
                        });
                        break;
                }
            }

            return sortingOptions;
        }

        #endregion
    }
}
