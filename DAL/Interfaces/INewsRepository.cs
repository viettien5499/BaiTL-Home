using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public partial interface INewsRepository
    {
        List<NewsModel> GetDataAll();

    }
}
