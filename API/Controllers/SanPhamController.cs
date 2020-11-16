using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private string _path;
        private ISanPhamBusiness _itemBusiness;
        public SanPhamController(ISanPhamBusiness itemBusiness, IConfiguration configuration)
        {
            _itemBusiness = itemBusiness;
            _path = configuration["AppSettings:PATH"];

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
            string Masp = "";
            
            if (formData.Keys.Contains("Masp") && !string.IsNullOrEmpty(Convert.ToString(formData["Masp"]))) { Masp = Convert.ToString(formData["Masp"]); }
            _itemBusiness.Delete(Masp);
            return Ok();
        }


        [Route("create-item")]
        [HttpPost]
        public SanPhamModel CreateItem([FromBody] SanPhamModel model)
        {
            if (model.Hinhanh != null)
            {
                var arrData = model.Hinhanh.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Hinhanh = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            model.Masp = Guid.NewGuid().ToString();

            _itemBusiness.Create(model);

            return model;
        }


        [Route("update-item")]
        [HttpPost]
        public SanPhamModel UpdateItem([FromBody] SanPhamModel model)
        {
            if (model.Hinhanh != null)
            {
                var arrData = model.Hinhanh.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Hinhanh = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _itemBusiness.Update(model);
            return model;
        }


        [Route("get-all")]
        [HttpGet]
        public IEnumerable<SanPhamModel> GetDataAll()
        {
            return _itemBusiness.GetDataAll();
        }

        [Route("get-same-item/{item_group_id}")]
        [HttpGet]
        public IEnumerable<SanPhamModel> GetDataSameItem(string Maloai)
        {
            return _itemBusiness.GetDataSameItem(Maloai);
        }

        //[Route("get-all")]
        //[HttpGet]
        //public IEnumerable<ItemModel> GetDatabAll()
        //{
        //    return _itemBusiness.GetDataAll();
        //}


        [Route("get-by-id/{id}")]
        [HttpGet]
        public SanPhamModel GetDatabyID(string id)
        {
            return _itemBusiness.GetDatabyID(id);
        }


        [Route("search1")]
        [HttpPost]
        public ResponseModel Search1([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Tensp = "";
                if (formData.Keys.Contains("Tensp") && !string.IsNullOrEmpty(Convert.ToString(formData["Tensp"]))) { Tensp = Convert.ToString(formData["Tensp"]); }
                long total = 0;
                var data = _itemBusiness.Search1(page, pageSize, out total, Tensp);
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

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Maloai = "";
                if (formData.Keys.Contains("Maloai") && !string.IsNullOrEmpty(Convert.ToString(formData["Maloai"]))) { Maloai = Convert.ToString(formData["Maloai"]); }
                long total = 0;
                var data = _itemBusiness.Search(page, pageSize, out total, Maloai);
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
        [Route("get-spkm")]
        [HttpGet]
        public IEnumerable<SanPhamModel> GetSpKm()
        {
            return _itemBusiness.GetSpKm();
        }
        [Route("get-sptot")]
        [HttpGet]
        public IEnumerable<SanPhamModel> GetSpTot()
        {
            return _itemBusiness.GetSpTot();
        }
    }
}
