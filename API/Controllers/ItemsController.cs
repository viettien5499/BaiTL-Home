
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ItemsController : ControllerBase
    {
        private string _path;
        private IItemBusiness _itemBusiness;
        public ItemsController(IItemBusiness itemBusiness)
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


        [Route("delete-item")]
        [HttpPost]
        public IActionResult DeleteItem([FromBody] Dictionary<string, object> formData)
        {
            string item_id = "";

            if (formData.Keys.Contains("item_id") && !string.IsNullOrEmpty(Convert.ToString(formData["item_id"]))) { item_id = Convert.ToString(formData["item_id"]); }
            _itemBusiness.Delete(item_id);
            return Ok();
        }


        [Route("create-item")]
        [HttpPost]
        public ItemModel CreateItem([FromBody] ItemModel model)
        {
            if (model.item_image != null)
            {
                var arrData = model.item_image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.item_image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            model.item_id = Guid.NewGuid().ToString();

            _itemBusiness.Create(model);

            return model;
        }


        [Route("update-item")]
        [HttpPost]
        public ItemModel UpdateItem([FromBody] ItemModel model)
        {
            if (model.item_image != null)
            {
                var arrData = model.item_image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.item_image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _itemBusiness.Update(model);
            return model;
        }


        [Route("get-all")]
        [HttpGet]
        public IEnumerable<ItemModel> GetDataAll()
        {
            return _itemBusiness.GetDataAll();
        }

        [Route("get-same-item/{item_group_id}")]
        [HttpGet]
        public IEnumerable<ItemModel> GetDataSameItem(string item_group_id)
        {
            return _itemBusiness.GetDataSameItem(item_group_id);
        }

        //[Route("get-all")]
        //[HttpGet]
        //public IEnumerable<ItemModel> GetDatabAll()
        //{
        //    return _itemBusiness.GetDataAll();
        //}


        [Route("get-by-id/{id}")]
        [HttpGet]
        public ItemModel GetDatabyID(string id)
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
                string item_name = "";
                if (formData.Keys.Contains("item_name") && !string.IsNullOrEmpty(Convert.ToString(formData["item_name"]))) { item_name = Convert.ToString(formData["item_name"]); }
                long total = 0;
                var data = _itemBusiness.Search1(page, pageSize, out total, item_name);
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
                string item_group_id = "";
                if (formData.Keys.Contains("item_group_id") && !string.IsNullOrEmpty(Convert.ToString(formData["item_group_id"]))) { item_group_id = Convert.ToString(formData["item_group_id"]); }
                long total = 0;
                var data = _itemBusiness.Search(page, pageSize, out total, item_group_id);
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
