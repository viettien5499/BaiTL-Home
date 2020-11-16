using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public partial interface ICTHoaDonBanBusiness
    {
        bool Create(Model.CTHoaDonBanModel model);
        CTHoaDonBanModel GetDatabyID(string id);
        List<CTHoaDonBanModel> GetDataAll();
    }
}
