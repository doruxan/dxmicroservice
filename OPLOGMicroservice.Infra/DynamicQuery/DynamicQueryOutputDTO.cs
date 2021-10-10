using DynamicQueryBuilder.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPLOGMicroservice.Infra.DynamicQuery
{
    public class DynamicQueryOutputDTO<T>
    {
        public DynamicQueryOutputDTO(DynamicQueryOptions options, IEnumerable<T> data)
        {
            this.Data = data;
            this.Count = options.PaginationOption?.DataSetCount ?? data.Count();
        }

        public DynamicQueryOutputDTO(DynamicQueryPagedDTO<T> dto)
        {
            this.Data = dto.Items;
            this.Count = dto.DataSetCount;
        }

        public IEnumerable<T> Data { get; set; }

        public int Count { get; set; }
    }
}
