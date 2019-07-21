using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.DTOs.Common
{
    public class Pager
    {
        public Pager(int totalItems, int currentPage = 1, int pageSize = 10, int maxPages = 10) {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalItems / pageSize + ((totalItems % pageSize > 0) ? 1 : 0);
            var pages = new List<int>();

            var minPages = Math.Min(TotalPages, maxPages);
            for (int i = 1; i <= minPages; i++)
            {
                pages.Add(i);
            }

            Pages = pages;
        }

        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int StartPage { get; }
        public int EndPage { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }
        public IEnumerable<int> Pages { get; }
    }

}
