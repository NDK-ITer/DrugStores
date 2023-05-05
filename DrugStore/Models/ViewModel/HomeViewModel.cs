using DrugStore.Models.Entities;
using X.PagedList;

namespace DrugStore.Models.ViewModel
{
    public class HomeViewModel
    {
        private readonly DrugStoreDbContext dbContext = new DrugStoreDbContext();
        public List<TinTuc> tinTucs { get; set; }
        public HomeViewModel()
        {
            tinTucs = dbContext.TinTucs.ToList();
        }
    }
}
