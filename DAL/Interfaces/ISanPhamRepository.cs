using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface ISanPhamRepository
    {
        bool Create(SanPhamModel model);
        bool Delete(string id);
        bool Update(SanPhamModel model);

        SanPhamModel GetDatabyID(string id);
        List<SanPhamModel> GetDataAll();

        List<SanPhamModel> GetDataSameItem(string Maloai);
        List<SanPhamModel> GetSpKM();
        List<SanPhamModel> GetSpTOT();

        List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string Maloai);
        List<SanPhamModel> Search1(int pageIndex, int pageSize, out long total, string Tensp);
    }
}
