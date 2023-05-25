using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrugStore.Areas.Admin.Data
{
    public class TinTucInput
    {
        public Guid idtintuc { get; set; }
        public string content { get; set; }
        public string cover { get; set; }
        public string description { get; set; }
        public DateTime? createdDate { get; set; }
        public string[] idtag  { get; set;}
        public List<SelectListItem> drptag { get; set; }    
    }
}
