using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Domain.Persistence;

namespace TvMaze.Application.Abstractions.Contracts.Cast
{
    public enum CastSortFieldEnum
    {
        Birthday
    }
    public class CastsSort
    {
        public static CastsSort Empty = new CastsSort();

        public CastsSort()
        {
            SortFields = new List<CastSortFieldEnum>();
            SortDirections = new List<SortDirectionEnum>();
        }

        public IList<CastSortFieldEnum> SortFields { get; set; }
        public IList<SortDirectionEnum> SortDirections { get; set; }
    }
}
