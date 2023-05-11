using DrugStore.Models.Entities;
using DrugStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class DoanhMucSanPhamViewComponent:ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DoanhMucSanPhamViewModel DoanhMucSP = new DoanhMucSanPhamViewModel();
            return View (DoanhMucSP);
        }
    }
}
