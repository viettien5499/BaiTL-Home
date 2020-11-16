using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public partial interface IHoaDonBanRepository
    {
        bool Create(HoaDonBanModel model);
        bool Delete(string id);
        bool Update(HoaDonBanModel model);

        HoaDonBanModel GetDatabyID(string id);
        List<HoaDonBanModel> GetDataAll();

        List<HoaDonBanModel> GetDataSameItem(string Makh);

        List<HoaDonBanModel> Search(int pageIndex, int pageSize, out long total, string Mahdb);
    }
}
