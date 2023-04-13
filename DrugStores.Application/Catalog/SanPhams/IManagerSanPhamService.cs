using DrugStores.Application.Catalog.SanPhams.DTOs;
using DrugStores.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.Catalog.SanPhams
{
    public interface IManagerSanPhamService
    {
        Task<int> Create(SanPhamCreateRequest request);
        Task<int> Update(SanPhamCreateRequest request);
        Task<int> Delete(Guid id);
        Task<List<SanPhamViewModel>> GetAll();
        Task<PagedViewModel<SanPhamViewModel>> GetAllPaging(string keyWord, int pageIndex, int pageSize);
    }
}
