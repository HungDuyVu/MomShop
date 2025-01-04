using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MOMShop.Utils
{
    public class Paging<T>
    {
        public List<T> Items { get; set; }
        public decimal TotalItems { get; set; }
    }

    public class PagingBase
    {
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }

        private string _keyword { get; set; }
        [FromQuery(Name = "keyword")]
        public string Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim();
        }

        [NotMapped]
        public int Skip
        {
            get
            {
                int skip = (PageNumber - 1) * PageSize;
                if (skip < 0)
                {
                    skip = 0;
                }
                return skip;
            }
        }
    }
}
