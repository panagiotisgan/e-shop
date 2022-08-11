﻿namespace eShop.WebApi.Models
{
    public class PaginationQuery
    {        
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
