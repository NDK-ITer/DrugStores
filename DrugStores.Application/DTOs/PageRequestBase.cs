using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.DTOs
{
    public class PageRequestBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
