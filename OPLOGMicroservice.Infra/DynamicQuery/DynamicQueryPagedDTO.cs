using DynamicQueryBuilder.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPLOGMicroservice.Infra.DynamicQuery
{
    public class DynamicQueryPagedDTO<T>
    {
        public DynamicQueryPagedDTO(DynamicQueryOptions options, IEnumerable<T> items)
        {
            this.Items = items;
            this.Count = options.PaginationOption?.Count > 0 ? options.PaginationOption.Count : items.Count();
            this.Offset = options.PaginationOption?.Offset ?? 0;
            this.DataSetCount = options.PaginationOption?.DataSetCount ?? items.Count();
        }

        public DynamicQueryPagedDTO()
        {

        }

        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }

        public int Offset { get; set; }

        public int DataSetCount { get; set; }
    }
}
