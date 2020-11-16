using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class SanPhamBusiness : ISanPhamBusiness
    {
        private ISanPhamRepository _res;
        public SanPhamBusiness(ISanPhamRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public bool Create(SanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(SanPhamModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public SanPhamModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<SanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<SanPhamModel> GetDataSameItem(string Maloai)
        {
            return _res.GetDataSameItem(Maloai);
        }

        public List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string Maloai)
        {
            return _res.Search(pageIndex, pageSize, out total, Maloai);
        }

        public List<SanPhamModel> Search1(int pageIndex, int pageSize, out long total, string Tensp)
        {
            return _res.Search1(pageIndex, pageSize, out total, Tensp);
        }
        public List<SanPhamModel> GetSpKm()
        {
            return _res.GetSpKM();
        }
        public List<SanPhamModel> GetSpTot()
        {
            return _res.GetSpTOT();
        }
    }
}

