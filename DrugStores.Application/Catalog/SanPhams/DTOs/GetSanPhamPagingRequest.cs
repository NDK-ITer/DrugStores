using DrugStores.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.Catalog.SanPhams.DTOs
{
    public class GetSanPhamPagingRequest:PageRequestBase
    {
        public string keyWord { get; set; }
        public List<Guid> MaDMs { get; set; }
    }
}
