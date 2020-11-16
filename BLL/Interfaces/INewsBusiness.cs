using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace BLL.Interfaces
{
    public partial interface INewsBusiness
    {
        List<NewsModel> GetData();
    }
}
