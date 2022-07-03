using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Dtos
{
    public class PagenatedListDto<T> where T : class
    {
        public IEnumerable<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public bool HasNext { get; }
        public bool HasPrev { get; }

        public PagenatedListDto(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            HasNext = PageIndex < TotalPages;
            HasPrev = PageIndex > 1;
        }
    }
}
