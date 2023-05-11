using DrugStore.Models.Entities;
using DrugStore.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Components
{
    public class CaNhanHoaViewComponent : ViewComponent
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext().Created();
        public async Task<IViewComponentResult> InvokeAsync(string idUser, int? page)
        {
            if (idUser != null)
            {
                if (page == null) { page = 1; }
                page = page < 1 ? 1 : page;
                int pageSize = 9;
                List<CT_CaNhanHoa> cT_CaNhanHoa = dbContext.CT_CaNhanHoas.Where(c => c.Id == idUser).OrderBy(c => c.SoLanXem).Take(5).ToList();
                TongHopLoaiSP tongHopLoaiSP = new TongHopLoaiSP();
                foreach (var item in cT_CaNhanHoa)
                {

                }
            }
            return View();
        }
    }
}
