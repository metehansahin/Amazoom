using System;
using System.Collections.Generic;
using System.Timers;

namespace Amazoom
{
    public class Truck
    {
        protected const double weightCapacity = 2000; //weight capacity: 2000 lbs
        protected const double thresholdWeight = 1750;
        public double currentWeight { get; private set; } //NOTE IS NOT THE ACTUAL WEIGHT. it's the weight after all the assigned orders are loaded
        public int truckType {get; private set;}//0: delivery, 1: restocking 
        public TileNode dockedLocation {get;protected set; }
        public bool isReady; //true when the all the assigned orders were actually loaded on to the truck or when all items are unloaded

        public List<Order> orderList { get; protected set; }
        public List<Order> currentOrderList { get; protected set; }

        protected bool hasEnoughOrders; //true when truck is assigned enough orders
       

        public List<Item> itemList;
        public double itemListWeight; 
        protected int currentItemNum;
        public int numAssignedRobots;  //numAssignedRobot * robot.Carrying capacity > currentWeight 
        protected bool isEmpty;

        public bool hasEnoughAssignedRobots;


        //for delivery truck
        public Truck()
        {
            orderList = new List<Order>();
            currentOrderList = new List<Order>();
            isReady = false;
            hasEnoughOrders = false;
            currentWeight = 0;
            dockedLocation = null;
            currentItemNum = 0;
            truckType = 0;

        }
        //for restocking truck
        public Truck(List<Item> itemList)
        {
            this.itemList = itemList;
            isEmpty = false;
            isReady = false;
            numAssignedRobots = 0;
            hasEnoughAssignedRobots = false;
            currentWeight = 0;
            dockedLocation = null;
            currentItemNum = 0;
            truckType = 1;
            foreach (Item item in itemList)
            {
                currentWeight += item.ItemWeight;
            }
            itemListWeight = currentWeight; 

        }

        public void assignDockLocation(TileNode dockedLocation)
        {
            this.dockedLocation = dockedLocation;
        }

        /*
       * Removes the next item in ItemList
       * 
       * @return next item in line or null if there are no items left in truck
       */

        public bool assignOrder(Order order)
        {
            if (currentWeight + order.OrderWeight > weightCapacity)
            {
                return false;
            }
            else if (currentWeight + order.OrderWeight > thresholdWeight)
            {
                hasEnoughOrders = true;
            }
            orderList.Add(order);
            currentWeight += order.OrderWeight;
            return true;
        }
        public bool loadOrder(Order order)
        {
            currentOrderList.Add(order);
            if (currentOrderList.Count == orderList.Count && hasEnoughOrders)
            {
                isReady = true;

                return true;
            }
            return false;

        }
        public double checkWeightUntilMax()
        {
            return weightCapacity - currentWeight;
        }

        public Item unloadItem()
        {
            if (!isEmpty)
            {
                Item currentItem = itemList[currentItemNum];
                currentWeight -= currentItem.ItemWeight;
                currentItemNum++; //move onto next item in Item List

                if (currentItemNum == (itemList.Count - 1))
                {
                    isEmpty = true;
                    isReady = true;
                    
                }
                return currentItem;
            }
            else
            {
                return null;
            }
        }

        /*
	    * Looks at next item in ItemList without removing it from the truck
	    * 
	    * @return next item or -1 if there are no items left in truck
	    */

        public double checkNextItemWeight()
        {
            if (!isEmpty)
            {
                Item currentItem = itemList[currentItemNum];
                return currentItem.ItemWeight;
            }
            else
            {
                return -1;
            }
        }
        public void resetTruck()
        {
            if (truckType == 0)
            {
                orderList.Clear();
                currentOrderList.Clear();
            }
            else{
                hasEnoughOrders = false;
                currentItemNum = 0;
            }
            
            isReady = false;
            
            currentWeight = 0;
            dockedLocation = null;
           

        }

    }
}

