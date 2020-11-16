using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class HoaDonBanModel
    {
        public string Mahdb { get; set; }
        public string Makh { get; set; }
        public DateTime Ngayban { get; set; }
        public string Hoten { get; set; }
        public string SDT { get; set; }
        public string Diachi { get; set; }
        public float Tongtien { get; set; }
        public string Thanhtoan { get; set; }
        public string Ghichu { get; set; }
        public List<CTHoaDonBanModel> listjson_chitiet { get; set; }
    }
}
