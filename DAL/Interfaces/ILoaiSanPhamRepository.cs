using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface ILoaiSanPhamRepository
    {
        List<LoaiSanPhamModel> GetData();
        bool Create(LoaiSanPhamModel model);
        bool Delete(string id);
        bool Update(LoaiSanPhamModel model);

        LoaiSanPhamModel GetDatabyID(string id);
        List<LoaiSanPhamModel> GetDataAll();

        List<LoaiSanPhamModel> GetDataSameItem(string Maloai);
        List<LoaiSanPhamModel> Search(int pageIndex, int pageSize, out long total, string Tenloai);
    }
}
