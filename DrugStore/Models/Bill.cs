namespace DrugStore.Models
{
    public class Bill
    {
        public Guid id { get;set; }
        public DateTime ngay { get;set; }
        public string trangthai { get;set; }
        public string tongtien { get;set; }
    }
}
