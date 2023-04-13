using DrugStores.Application.Catalog.SanPhams.DTOs;
using DrugStores.Application.DTOs;
using DrugStores.Data.EF;
using DrugStores.Data.Entities;
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
        public async Task<int> Create(SanPhamCreateRequest request)
        {
            var sanPham = new SanPham()
            {
                MaSP = request.MaSP,
                TenSP = request.TenSP,
                DonViTinh = request.DonViTinh,
                ThanhPhan = request.ThanhPhan,
                CongDung = request.CongDung,
                LieuDung = request.LieuDung,
                TacDungPhu = request.TacDungPhu,
                MoTa = request.MoTa,
                AnhDaiDien = request.AnhDaiDien,
                SoLanMua = request.SoLanMua,
                GiamGia = request.GiamGia,
                DonGia = request.DonGia,
                MaDM = request.MaDM,
                MaTT = request.MaTT,
                MaLoaiSP = request.MaLoaiSP,
            };
            dbContext.SanPhams.Add(sanPham);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
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

        public Task<int> Update(SanPhamCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
