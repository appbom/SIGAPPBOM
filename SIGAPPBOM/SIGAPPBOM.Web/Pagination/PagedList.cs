using System;
using System.Collections.Generic;

namespace SIGAPPBOM.Web.Pagination
{
    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IEnumerable<T> requerimientos,
                         int totalItems, int currentPage,
                         int pageSize)
        {
            this.AddRange(requerimientos);  // se agrega una lista

            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.ItemsPerPage = pageSize;
        }

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return CurrentPage > 1;
            }

        }

        public bool HasNextPage
        {
            get
            {
                return CurrentPage < TotalPages;

            }
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}