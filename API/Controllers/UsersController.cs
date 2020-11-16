
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
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserBusiness _userBusiness;
        private string _path;
        public UsersController(IUserBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;

            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userBusiness.Authenticate(model.Username, model.Password);
            if (user == null)

                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);

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

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<UserModel> GetDataAll()
        {
            return _userBusiness.GetDataAll();
        }

        [Route("delete-user")]
        [HttpPost]
        public IActionResult DeleteUser([FromBody] Dictionary<string, object> formData)
        {
            string user_id = "";

            if (formData.Keys.Contains("user_id") && !string.IsNullOrEmpty(Convert.ToString(formData["user_id"]))) { user_id = Convert.ToString(formData["user_id"]); }
            _userBusiness.Delete(user_id);
            return Ok();
        }
        

        [Route("create-user")]
        [HttpPost]
        public UserModel CreateUser([FromBody] UserModel model)
        {
            if (model.image_url != null)
            {
               var arrData = model.image_url.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.image_url = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }

            model.user_id = Guid.NewGuid().ToString();

            _userBusiness.Create(model);

            return model;
        }
        

        [Route("update-user")]
        [HttpPost]
        public UserModel UpdateUser([FromBody] UserModel model)
        {
            if (model.image_url != null)
            {
                var arrData = model.image_url.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                   model.image_url = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _userBusiness.Update(model);
            return model;
        }
        
        [Route("get-by-id/{id}")]
        [HttpGet]
        public UserModel GetDatabyID(string id)
        {
            return _userBusiness.GetDatabyID(id);
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

                string hoten = "";

                if (formData.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formData["hoten"]))) { hoten = Convert.ToString(formData["hoten"]); }

                string taikhoan = "";

                if (formData.Keys.Contains("taikhoan") && !string.IsNullOrEmpty(Convert.ToString(formData["taikhoan"]))) { hoten = Convert.ToString(formData["taikhoan"]); }

                long total = 0;

                var data = _userBusiness.Search(page, pageSize, out total, hoten, taikhoan);

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
