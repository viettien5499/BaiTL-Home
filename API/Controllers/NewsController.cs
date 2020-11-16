

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _Business;
        public NewsController(INewsBusiness bus)
        {
            _Business = bus;
        }
        [Route("get_tintuc_new")]
        [HttpGet]
        public List<NewsModel> get_Tintuc_New()
        {
            return _Business.GetData();
        }
    }
}