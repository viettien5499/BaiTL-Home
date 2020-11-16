using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IItemBusiness
    {
        bool Create(ItemModel model);
        bool Update(ItemModel model);
        bool Delete(string id);
        ItemModel GetDatabyID(string id);
        List<ItemModel> GetDataAll();

        List<ItemModel> GetDataSameItem(string item_group_id);
        List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id);
        List<ItemModel> Search1(int pageIndex, int pageSize, out long total, string item_group_id);

    }
}
