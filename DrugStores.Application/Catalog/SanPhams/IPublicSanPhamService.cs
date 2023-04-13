using DrugStores.Application.Catalog.SanPhams.DTOs;
using DrugStores.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.Catalog.SanPhams
{
    public interface IPublicSanPhamService
    {
        PagedViewModel<SanPhamViewModel> GetAllByMaDM(Guid MaDM, int pageIndex, int pageSize);
    }
}
