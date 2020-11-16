using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface IKhachHangRepository
    {
        bool Create(KhachHangModel model);
        bool Delete(string id);
        bool Update(KhachHangModel model);

        KhachHangModel GetDatabyID(string id);
        List<KhachHangModel> GetDataAll();

        List<KhachHangModel> GetDataSameItem(string Makh);
        List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string Hoten);
    }
}
