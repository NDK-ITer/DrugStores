using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace DrugStore.Areas.Admin.Data
{
    public class TinTucInput
    {
        public Guid idtintuc { get; set; }
        [DisplayName("Nội dung")]
        public string content { get; set; }
        [DisplayName("Hình ảnh")]
        public string cover { get; set; }
        [DisplayName("Mô tả")]
        public string description { get; set; }
        public DateTime? createdDate { get; set; }
        public string[] idtag  { get; set;}
        public List<SelectListItem> drptag { get; set; }    
    }
}
