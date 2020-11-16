using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class LoaiSanPhamBusiness : ILoaiSanPhamBusiness
    {
        private ILoaiSanPhamRepository _res;
        public LoaiSanPhamBusiness(ILoaiSanPhamRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public List<LoaiSanPhamModel> getallLoai()
        {
            return _res.GetData();

        }
        public bool Create(LoaiSanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(LoaiSanPhamModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public LoaiSanPhamModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<LoaiSanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<LoaiSanPhamModel> GetDataSameItem(string Maloai)
        {
            return _res.GetDataSameItem(Maloai);
        }

        public List<LoaiSanPhamModel> Search(int pageIndex, int pageSize, out long total, string Tenloai)
        {
            return _res.Search(pageIndex, pageSize, out total, Tenloai);
        }

    }
}

