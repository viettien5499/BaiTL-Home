using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonBanController : ControllerBase
    {
        private IHoaDonBanBusiness _hoaDonBusiness;
        private string _path;

        public HoaDonBanController(IHoaDonBanBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("delete-item")]
        [HttpPost]
        public IActionResult DeleteItem([FromBody] Dictionary<string, object> formData)
        {
            string Mahdb = "";

            if (formData.Keys.Contains("Mahdb") && !string.IsNullOrEmpty(Convert.ToString(formData["Mahdb"]))) { Mahdb = Convert.ToString(formData["Mahdb"]); }
            _hoaDonBusiness.Delete(Mahdb);
            return Ok();
        }
        
        [Route("create-hoadonban")]
        [HttpPost]
        public HoaDonBanModel CreateItem([FromBody] HoaDonBanModel model)
        {
            model.Mahdb = Guid.NewGuid().ToString();
            if (model.listjson_chitiet != null)
            {
                foreach (var item in model.listjson_chitiet)
                    item.Macthdb = Guid.NewGuid().ToString();
            }
            _hoaDonBusiness.Create(model);
            return model;
        } 
     /*
        [Route("create-hoadonban")]
        [HttpPost]
        public HoaDonBanModel CreateItem([FromBody] HoaDonBanModel model)
        {
            _hoaDonBusiness.Create(model);
            return model;
        } 
        */

        [Route("update-hoadonban")]
        [HttpPost]
        public HoaDonBanModel UpdateItem([FromBody] HoaDonBanModel model)
        {
            if (model.Mahdb != null)
            {
                var arrData = model.Mahdb.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Mahdb = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _hoaDonBusiness.Update(model);
            return model;
        }


        [Route("get-all")]
        [HttpGet]
        public IEnumerable<HoaDonBanModel> GetDataAll()
        {
            return _hoaDonBusiness.GetDataAll();
        }

        [Route("get-same-item/{item_group_id}")]
        [HttpGet]
        public IEnumerable<HoaDonBanModel> GetDataSameItem(string Makh)
        {
            return _hoaDonBusiness.GetDataSameItem(Makh);
        }

        //[Route("get-all")]
        //[HttpGet]
        //public IEnumerable<ItemModel> GetDatabAll()
        //{
        //    return _itemBusiness.GetDataAll();
        //}


        [Route("get-by-id/{id}")]
        [HttpGet]
        public HoaDonBanModel GetDatabyID(string id)
        {
            return _hoaDonBusiness.GetDatabyID(id);
        }


        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Mahdb = "";
                if (formData.Keys.Contains("Mahdb") && !string.IsNullOrEmpty(Convert.ToString(formData["Mahdb"]))) { Mahdb = Convert.ToString(formData["Mahdb"]); }
                long total = 0;
                var data = _hoaDonBusiness.Search(page, pageSize, out total, Mahdb);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
