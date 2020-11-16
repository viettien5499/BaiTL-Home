using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class ItemBusiness : IItemBusiness
    {
        private IItemRepository _res;
        public ItemBusiness(IItemRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public bool Create(ItemModel model)
        {
            return _res.Create(model);
        }
        public bool Update(ItemModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public ItemModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<ItemModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public List<ItemModel> GetDataSameItem(string item_group_id)
        {
            return _res.GetDataSameItem(item_group_id);
        }

        public List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id)
        {
            return _res.Search(pageIndex, pageSize, out total, item_group_id);
        }

        public List<ItemModel> Search1(int pageIndex, int pageSize, out long total, string item_group_id)
        {
            return _res.Search1(pageIndex, pageSize, out total, item_group_id);
        }
    }

}
