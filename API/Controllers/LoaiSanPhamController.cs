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
    public class LoaiSanPhamController : ControllerBase
    {
        private ILoaiSanPhamBusiness ILoaiSanPham;
        private string _path;

        public LoaiSanPhamController(ILoaiSanPhamBusiness ILoaiSP)
        {
            ILoaiSanPham = ILoaiSP;
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
        // GET: api/<LoaiSanPhamController>
        [Route("getLoaisp")]
        [HttpGet]
        public IEnumerable<LoaiSanPhamModel> Get()
        {
            return ILoaiSanPham.getallLoai();
        }

        [Route("delete-lsp")]
        [HttpPost]
        public IActionResult DeleteItem([FromBody] Dictionary<string, object> formData)
        {
            string Maloai = "";

            if (formData.Keys.Contains("Maloai") && !string.IsNullOrEmpty(Convert.ToString(formData["Maloai"]))) { Maloai = Convert.ToString(formData["Maloai"]); }
            ILoaiSanPham.Delete(Maloai);
            return Ok();
        }


        [Route("create-lsp")]
        [HttpPost]
        public LoaiSanPhamModel CreateItem([FromBody] LoaiSanPhamModel model)
        {
            if (model.Tenloai != null)
            {
                var arrData = model.Tenloai.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Tenloai = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            model.Maloai = Guid.NewGuid().ToString();

            ILoaiSanPham.Create(model);

            return model;
        }


        [Route("update-lsp")]
        [HttpPost]
        public LoaiSanPhamModel UpdateItem([FromBody] LoaiSanPhamModel model)
        {
            if (model.Tenloai != null)
            {
                var arrData = model.Tenloai.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Tenloai = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            ILoaiSanPham.Update(model);
            return model;
        }


        [Route("get-all")]
        [HttpGet]
        public IEnumerable<LoaiSanPhamModel> GetDataAll()
        {
            return ILoaiSanPham.GetDataAll();
        }

        [Route("get-same-item/{item_group_id}")]
        [HttpGet]
        public IEnumerable<LoaiSanPhamModel> GetDataSameItem(string Maloai)
        {
            return ILoaiSanPham.GetDataSameItem(Maloai);
        }

        //[Route("get-all")]
        //[HttpGet]
        //public IEnumerable<ItemModel> GetDatabAll()
        //{
        //    return _itemBusiness.GetDataAll();
        //}


        [Route("get-by-id/{id}")]
        [HttpGet]
        public LoaiSanPhamModel GetDatabyID(string id)
        {
            return ILoaiSanPham.GetDatabyID(id);
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
                string Tenloai = "";
                if (formData.Keys.Contains("Tenloai") && !string.IsNullOrEmpty(Convert.ToString(formData["Tenloai"]))) { Tenloai = Convert.ToString(formData["Tenloai"]); }
                long total = 0;
                var data = ILoaiSanPham.Search(page, pageSize, out total, Tenloai);
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

