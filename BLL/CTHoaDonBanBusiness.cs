using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CTHoaDonBanBusiness : ICTHoaDonBanBusiness
    {
        private CTHoaDonBanRepository _res;
        public CTHoaDonBanBusiness(CTHoaDonBanRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public bool Create(CTHoaDonBanModel model)
        {
            return _res.Create(model);
        }
        public CTHoaDonBanModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<CTHoaDonBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<CTHoaDonBanModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
