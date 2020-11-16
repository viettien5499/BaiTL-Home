using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IKhachHangBusiness
    {
        bool Create(KhachHangModel model);
        bool Update(KhachHangModel model);
        bool Delete(string id);
        KhachHangModel GetDatabyID(string id);
        List<KhachHangModel> GetDataAll();
        List<KhachHangModel> GetDataSameItem(string Makh);
        List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string Hoten);
    }
}
