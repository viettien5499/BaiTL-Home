using System;
using System.Collections.Generic;
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
    public class CTHoaDonBanController : ControllerBase
    {
        private ICTHoaDonBanBusiness _order;
        public CTHoaDonBanController(ICTHoaDonBanBusiness itemBusiness)
        {
            _order = itemBusiness;
        }

        [Route("create-item")]
        [HttpPost]
        public Model.CTHoaDonBanModel CreateItem([FromBody] CTHoaDonBanModel model)
        {
            _order.Create(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public CTHoaDonBanModel GetDatabyID(string id)
        {
            return _order.GetDatabyID(id);
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<CTHoaDonBanModel> GetDatabAll()
        {
            return _order.GetDataAll();
        }
    }
}
