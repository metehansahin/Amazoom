using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazoomMain
{
    public static class ItemProcessor
    {
       public static int CreateItem(int itemID, double itemWeight, double itemVolume, string itemName, int stock)
        {
            ItemModel data = new ItemModel
            {
                ItemID = itemID,
                ItemWeight = itemWeight,
                ItemVolume = itemVolume,
                ItemName = itemName,
                Stock = stock
            };

            string sql = @"insert into dbo.Item (ItemID, ItemWeight, ItemVolume, ItemName, Stock) 
                        values (@ItemID, @ItemWeight, @ItemVolume, @ItemName, @Stock);";

            return SqlDataAccess.SaveData(sql, data);
        } 
        public static List<ItemModel> LoadItems()
        { 
            string sql = @"select Id, ItemID, ItemWeight, ItemVolume, ItemName, Stock
                        from dbo.Item;";
            return SqlDataAccess.LoadData<ItemModel>(sql);
        }

        public static void UpdateItemStock(int itemID, int stock)
        {
            ItemModel data = new ItemModel
            {
                ItemID = itemID,
                Stock = stock
            };

            string sql = @"UPDATE Item SET Stock = @Stock WHERE ItemID = @ItemID;";
            SqlDataAccess.SaveData(sql, data);
        }
    }
}
