using DrugStores.Application.Catalog.SanPhams.DTOs;
using DrugStores.Application.DTOs;
using DrugStores.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStores.Application.Catalog.SanPhams
{
    public class ManagerSanPhamService : IManagerSanPhamService
    {
        private readonly DrugStoreDbContext dbContext;
        public ManagerSanPhamService(DrugStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Guid> Create(SanPhamCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SanPhamViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PagedViewModel<SanPhamViewModel>> GetAllPaging(string keyWord, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(SanPhamCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
