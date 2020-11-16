using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface IHoaDonBanBusiness
    {
        bool Create(HoaDonBanModel model);
        bool Update(HoaDonBanModel model);
        bool Delete(string id);
        HoaDonBanModel GetDatabyID(string id);
        List<HoaDonBanModel> GetDataAll();
        List<HoaDonBanModel> GetDataSameItem(string Makh);
        List<HoaDonBanModel> Search(int pageIndex, int pageSize, out long total, string Mahdb);
    }
}
