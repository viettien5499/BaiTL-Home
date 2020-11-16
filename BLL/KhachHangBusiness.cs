using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class KhachHangBusiness : IKhachHangBusiness
    {
        private IKhachHangRepository _res;
        public KhachHangBusiness(IKhachHangRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public bool Create(KhachHangModel model)
        {
            return _res.Create(model);
        }
        public bool Update(KhachHangModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public KhachHangModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<KhachHangModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<KhachHangModel> GetDataSameItem(string Makh)
        {
            return _res.GetDataSameItem(Makh);
        }

        public List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string Hoten)
        {
            return _res.Search(pageIndex, pageSize, out total, Hoten);
        }
    }
}
