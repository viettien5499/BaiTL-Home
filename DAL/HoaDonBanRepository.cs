using DAL.Helper;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class HoaDonBanRepository : IHoaDonBanRepository
    {
        private IDatabaseHelper _dbHelper;
        public HoaDonBanRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(HoaDonBanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_hoadonban_create",
                
                "@Makh", model.Makh,
                "@Ngayban", DateTime.Now,
                "@Hoten", model.Hoten,
                "@SDT", model.SDT,
                "@Diachi", model.Diachi,
                "@Tongtien", model.Tongtien,
                "@Thanhtoan", model.Thanhtoan,
                "@Ghichu", model.Ghichu,
                "@listjson_chitiet", model.listjson_chitiet != null ? MessageConvert.SerializeObject(model.listjson_chitiet) : null);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_hoadonban_delete",
                "@Mahdb", id);
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
        public bool Update(HoaDonBanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_hoadonban_update",
                "@Mahdb", model.Mahdb,
                "@Makh", model.Makh,
                "@Ngayban", model.Ngayban,
                "@Hoten", model.Ngayban,
                "@SDT", model.Ngayban,
                "@Diachi", model.Ngayban,
                "@Tongtien", model.Tongtien,
                "@Thanhtoan", model.Thanhtoan,
                "@Ghichu", model.Ghichu,
                "@listjson_chitiet", model.listjson_chitiet != null ? MessageConvert.SerializeObject(model.listjson_chitiet) : null);
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


        public HoaDonBanModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_hoadonban_get_by_id",
                     "@Mahdb", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HoaDonBanModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HoaDonBanModel> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_hoadonban_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HoaDonBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HoaDonBanModel> GetDataSameItem(string Makh)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_hoadonban_same_type", "@item_group_id", Makh);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<HoaDonBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HoaDonBanModel> Search(int pageIndex, int pageSize, out long total, string Mahdb)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_hoadonban_searchadmin",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Mahdb", Mahdb);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<HoaDonBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
