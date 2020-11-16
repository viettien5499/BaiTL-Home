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
    public class KhachHangController : ControllerBase
    {
        private string _path;
        private IKhachHangBusiness _itemBusiness;
        public KhachHangController(IKhachHangBusiness itemBusiness)
        {
            _itemBusiness = itemBusiness;
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


        [Route("delete-kh")]
        [HttpPost]
        public IActionResult DeleteItem([FromBody] Dictionary<string, object> formData)
        {
            string Makh = "";

            if (formData.Keys.Contains("Makh") && !string.IsNullOrEmpty(Convert.ToString(formData["Makh"]))) { Makh = Convert.ToString(formData["Makh"]); }
            _itemBusiness.Delete(Makh);
            return Ok();
        }


        [Route("create-kh")]
        [HttpPost]
        public KhachHangModel CreateItem([FromBody] KhachHangModel model)
        {
            if (model.Hoten != null)
            {
                var arrData = model.Hoten.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Hoten = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            model.Makh = Guid.NewGuid().ToString();

            _itemBusiness.Create(model);

            return model;
        }


        [Route("update-kh")]
        [HttpPost]
        public KhachHangModel UpdateItem([FromBody] KhachHangModel model)
        {
            if (model.Hoten != null)
            {
                var arrData = model.Hoten.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Hoten = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _itemBusiness.Update(model);
            return model;
        }


        [Route("get-all")]
        [HttpGet]
        public IEnumerable<KhachHangModel> GetDataAll()
        {
            return _itemBusiness.GetDataAll();
        }

        [Route("get-same-item/{item_group_id}")]
        [HttpGet]
        public IEnumerable<KhachHangModel> GetDataSameItem(string Makh)
        {
            return _itemBusiness.GetDataSameItem(Makh);
        }

        //[Route("get-all")]
        //[HttpGet]
        //public IEnumerable<ItemModel> GetDatabAll()
        //{
        //    return _itemBusiness.GetDataAll();
        //}


        [Route("get-by-id/{id}")]
        [HttpGet]
        public KhachHangModel GetDatabyID(string id)
        {
            return _itemBusiness.GetDatabyID(id);
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
                string Hoten = "";
                if (formData.Keys.Contains("Hoten") && !string.IsNullOrEmpty(Convert.ToString(formData["Hoten"]))) { Hoten = Convert.ToString(formData["Hoten"]); }
                long total = 0;
                var data = _itemBusiness.Search(page, pageSize, out total, Hoten);
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
