using DAL.Helper;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class KhachHangRepository : IKhachHangRepository
    {
        private IDatabaseHelper _dbHelper;
        public KhachHangRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(KhachHangModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_khachhang_create",
                "@Makh", model.Makh,
                "@Hoten", model.Hoten,
                "@Gioitinh", model.Gioitinh,
                "@Email", model.Email,
                "@SDT", model.SDT,
                "@Diachi", model.Diachi,
                "@Thanhtoan", model.Thanhtoan,
                "@Ghichu", model.Thanhtoan,
                "@Password", model.Password);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_khachhang_delete",
                "@Makh", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(KhachHangModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_khachhang_update",
              "@Makh", model.Makh,
                "@Hoten", model.Hoten,
                "@Gioitinh", model.Gioitinh,
                "@Email", model.Email,
                "@SDT", model.SDT,
                "@Diachi", model.Diachi,
                "@Thanhtoan", model.Thanhtoan,
                "@Ghichu", model.Thanhtoan,
                "@Password", model.Password);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KhachHangModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_khachhang_get_by_id",
                     "@Makh", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<KhachHangModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<KhachHangModel> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_khachhang_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<KhachHangModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KhachHangModel> GetDataSameItem(string Makh)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_khachhang_same_type", "@item_group_id", Makh);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<KhachHangModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KhachHangModel> Search(int pageIndex, int pageSize, out long total, string Hoten)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_khachhang_searchadmin",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Hoten", Hoten);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<KhachHangModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
