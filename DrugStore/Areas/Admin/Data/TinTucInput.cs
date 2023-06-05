using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrugStore.Areas.Admin.Data
{
    public class TinTucInput
    {
        public Guid idtintuc { get; set; }
        [DisplayName("Nội dung")]
        [Required]
        public string content { get; set; }
        [DisplayName("Hình ảnh")]
        [Required]
        public string cover { get; set; }
        [DisplayName("Mô tả")]
        [Required]
        public string description { get; set; }
        [DisplayName("Ngày tạo")]
        public DateTime? createdDate { get; set; }
        public string[] idtag  { get; set;}
        public List<SelectListItem> drptag { get; set; }    
    }
}
