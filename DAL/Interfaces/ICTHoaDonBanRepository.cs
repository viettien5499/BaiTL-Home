using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICTHoaDonBanRepository
    {
        bool Create(CTHoaDonBanModel model);
        CTHoaDonBanModel GetDatabyID(string id);
        public List<CTHoaDonBanModel> GetDataAll();
    }
}
