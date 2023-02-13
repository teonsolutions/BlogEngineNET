using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlogEngine.Common
{
    public class PaginatedResponse<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; set; }
        public int TotalPages { get; set; }
        public PaginatedResponse()
        {

        }
        [JsonConstructor]
        public PaginatedResponse(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data, int totalPages)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
            this.TotalPages = totalPages;
        }
    }
}
