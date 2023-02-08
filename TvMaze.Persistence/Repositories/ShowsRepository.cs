using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Domain.Models;
using TvMaze.Persistence.Abstractions.Repositories;
using TvMaze.Persistence.EntityFramework;

namespace TvMaze.Persistence.Repositories
{
    internal class ShowsRepository: EfEntityRepositoryBase<Show, Guid>, IShowsRepository
    {
        public ShowsRepository(IEfUnitOfWork unitOfWork, IEfQueryBuilder queryBuilder) : base(unitOfWork, queryBuilder)
        {
        }
    }
}
