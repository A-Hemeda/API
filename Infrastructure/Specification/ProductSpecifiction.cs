using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification
{
    public class ProductSpecifiction
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxSize = 50;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxSize) ? MaxSize : value;
        }
        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower().Trim(); }
        }


    }
}
