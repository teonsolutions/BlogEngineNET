using System;
using System.Linq;

namespace BlogEngine.Common
{
    public static class QueryableExtensions
    {
        public static PaginatedResponse<TResponse> ToPagedResponse<TResponse, TRequest>(this IQueryable<TResponse> source, PaginatedRequest<TRequest> pageRequest)
            where TRequest : class
            where TResponse : class
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (pageRequest == null)
                throw new ArgumentNullException("pageRequest");

            return new PaginatedResponse<TResponse>(
                pageIndex: (pageRequest.Skip / pageRequest.PageSize) + 1,
                pageSize: pageRequest.PageSize,
                count: source.Count(),
                data: source.Skip(pageRequest.Skip).Take(pageRequest.PageSize).ToList(),
                totalPages: 0);
        }
    }
}
