using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Application.Abstractions.Contracts.Cast;
using TvMaze.Domain.Persistence;

namespace TvMaze.Application.Abstractions.Contracts.Shows
{
    public enum ShowsSortFieldEnum
    {
        ShowId
    }
    public class ShowsSort
    {
        public static ShowsSort Empty = new ShowsSort();

        public ShowsSort()
        {
            SortFields = new List<ShowsSortFieldEnum>();
            SortDirections = new List<SortDirectionEnum>();
        }

        public IList<ShowsSortFieldEnum> SortFields { get; set; }
        public IList<SortDirectionEnum> SortDirections { get; set; }
    }
}
