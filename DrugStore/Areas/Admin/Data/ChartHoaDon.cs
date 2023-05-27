using System.Data;

namespace DrugStore.Areas.Admin.Data
{
    public class ChartHoaDon
    {
        public Int64 gio { get; set; }
        public Int32 soluong { get; set; }

        public ChartHoaDon(DataRow dataRow)
        {

            this.gio = (Int64)dataRow["sogio"];
            this.soluong = (Int32)dataRow["soluong"];
        }
    }
}
