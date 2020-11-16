using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class NewsBusiness : INewsBusiness
    {
        private INewsRepository _res;
        public NewsBusiness(INewsRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }

        public List<NewsModel> GetData()
        {
            return _res.GetDataAll();
        }


    }

}
