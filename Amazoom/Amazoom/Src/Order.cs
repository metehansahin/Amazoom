using System;
using System.Collections.Generic;
using System.Text;

namespace Amazoom
{
    public class Order
    {
        private int orderNum;
        private double orderWeight; //total weight of all items in order
        private List<Item> itemList = new List<Item>(); //assume add item to order one at a time
        private bool isComfirmed = false;
        private bool isProccessed = false;
        private Warehouse homeWarehouse;
        public Truck assignedTruck { get; set; } 

        //TO ADD: priority, item max?

        public Order()
        {
            orderNum = 0;
            orderWeight = 0;
        }

        public Order(int OrderNumber)
        {
            orderNum = OrderNumber;
            orderWeight = 0;
        }

        // getters and setters
        // For how to use them: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.getaccessors?view=net-6.0
        public int OrderNum { get => orderNum; set => orderNum = value; }
        public double OrderWeight { get => orderWeight; set => orderWeight = value; }
        public List<Item> ItemList { get => itemList; set => itemList = value; }
        public bool IsComfirmed { get => isComfirmed; set => isComfirmed = value; }
        public bool IsProccessed { get => isProccessed; set => isProccessed = value; }


        public void addItemtoOrder(Item NewItem, int Quantity) //this means we need some master dictionary where item ID is connected item object
        {
            for (int i = 0; i < Quantity; i++)
            {
                //check if avialable and reserves the item 
                /*
                //MUTEX LOCK ??use try finally block??? see synchronization lecture example
                if (homeWarehouse.itemQuantity[NewItem.ItemID] > 0)
                {
                    homeWarehouse.itemQuantity[NewItem.ItemID]--;
                }
                //MUTEX RELEASE
                */

                itemList.Add(NewItem);
                orderWeight += NewItem.ItemWeight;

                
            }

        }

        public void removeItemfromOrder(Item NewItem, int Quantity)
        {
            for (int i = 0; i < Quantity; i++)
            {
                //add item back to quantity list 
                homeWarehouse.itemQuantity[NewItem.ItemID]++;

                itemList.Add(NewItem);
                orderWeight += NewItem.ItemWeight;

                
            }

        }
    }
}
