using System.Data;

namespace DrugStore.Areas.Admin.Data
{
    public class ChartTongTien
    {
        public string ngay { get; set; }
        public decimal tongtien { get; set; }

        public ChartTongTien(DataRow dataRow)
        {

            this.ngay = (string)dataRow["ngay"];
            this.tongtien = (decimal)dataRow["tongtien"];
        }
    }
}
