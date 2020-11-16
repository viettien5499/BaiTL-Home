using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ILoaiSanPhamBusiness
    {
        public List<LoaiSanPhamModel> getallLoai();
        bool Create(LoaiSanPhamModel model);
        bool Update(LoaiSanPhamModel model);
        bool Delete(string id);
        LoaiSanPhamModel GetDatabyID(string id);
        List<LoaiSanPhamModel> GetDataAll();
        List<LoaiSanPhamModel> GetDataSameItem(string Maloai);
        List<LoaiSanPhamModel> Search(int pageIndex, int pageSize, out long total, string Tenloai);
    }
}
