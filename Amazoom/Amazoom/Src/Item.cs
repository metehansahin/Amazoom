using System;
using System.Collections.Generic;
using System.Text;

namespace Amazoom
{
    public class Item
    {

        // public int itemQuantity; //decide how to account for quantity
        private double itemWeight;
        private string itemName;
        private int itemID;
        private int orderID;
        private int shelfNum;
        public bool willBeStocked;
        //TO ADD: LOCATION OF ITEM IN WAREHOUSE
        public Item()
        {
            //itemQuantity = 0;
            itemWeight = 0;
            itemName = "";
            itemID = 0;
            orderID=0;
            willBeStocked = false;
        }

        public Item(int Weight, string Name, int ItemID, int OrderID) //int Quantity,
        {
            //itemQuantity = Quantity;
            itemWeight = Weight;
            itemName = Name;
            itemID = ItemID;
            orderID = OrderID;
            willBeStocked = false;

        }

        // getters and setters
        // For how to use them: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.getaccessors?view=net-6.0
        public double ItemWeight { get => itemWeight; set => itemWeight = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public int ItemID { get => itemID; set => itemID = value; }
        public int OrderID { get => orderID; set => orderID = value; }
        public int ShelfNum { get => shelfNum; set => shelfNum = value; }
    }
}
