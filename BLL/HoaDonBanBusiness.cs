using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class HoaDonBanBusiness : IHoaDonBanBusiness
    {
        private IHoaDonBanRepository _res;
        public HoaDonBanBusiness(IHoaDonBanRepository res)
        {
            _res = res;
        }
        public bool Create(HoaDonBanModel model)
        {
            return _res.Create(model);
        }
        public bool Update(HoaDonBanModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public HoaDonBanModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<HoaDonBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<HoaDonBanModel> GetDataSameItem(string Makh)
        {
            return _res.GetDataSameItem(Makh);
        }

        public List<HoaDonBanModel> Search(int pageIndex, int pageSize, out long total, string Mahdb)
        {
            return _res.Search(pageIndex, pageSize, out total, Mahdb);
        }
    }
}

