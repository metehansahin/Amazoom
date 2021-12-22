using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazoomMain
{
    public static class BundleProcessor
    {
        public static int CreateBundle(int itemID, string itemName, int quantity, int orderID)
        {
            BundleModel data = new BundleModel
            {
                ItemID = itemID,
                ItemName = itemName,
                Quantity = quantity,
                OrderID = orderID
            };

            string sql = @"insert into dbo.Bundle (ItemID, ItemName, Quantity, OrderID) 
                        values (@ItemID, @ItemName, @Quantity, @OrderID);";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<BundleModel> LoadBundles()
        {
            string sql = @"select Id, ItemID, ItemName, Quantity, OrderID
                        from dbo.Bundle;";
            return SqlDataAccess.LoadData<BundleModel>(sql);
        }
    }
}
