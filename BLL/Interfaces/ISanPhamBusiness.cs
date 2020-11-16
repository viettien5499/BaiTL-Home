using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ISanPhamBusiness
    {
        bool Create(SanPhamModel model);
        bool Update(SanPhamModel model);
        bool Delete(string id);
        SanPhamModel GetDatabyID(string id);
        List<SanPhamModel> GetDataAll();
        List<SanPhamModel> GetSpKm();
        List<SanPhamModel> GetSpTot();
        List<SanPhamModel> GetDataSameItem(string Maloai);
        List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string Maloai);
        List<SanPhamModel> Search1(int pageIndex, int pageSize, out long total, string Tensp);
    }
}
