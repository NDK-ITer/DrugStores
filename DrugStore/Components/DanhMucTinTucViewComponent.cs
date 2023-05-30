using DrugStore.Models.Entities;
using DrugStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class DanhMucTinTucViewComponent:ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(dbContext.Tags.ToList());
        }
    }
}
