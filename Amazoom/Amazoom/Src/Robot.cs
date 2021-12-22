using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Amazoom
{
    public class Robot
    {
        // carrying capacity in kg
        private const double CARRYING_CAPACITY = 400;
        private const int TIME_TO_MOVE= 500;
        private const int TIME_TO_ADD_ITEM = 1000;

        private double speed;
        private double batteryPercentage;
        private double totalWeightOfItemsCarried;
        private int locationX;
        private int locationY;
        private bool isStocking;
        private bool isLoading;
        private Warehouse homeWarehouse;

        private List<Item> itemsCarried;

        //for debugging
        private char robotSymbol; //TO ADD TO CONSTURCTOR

        // default constructor: initializes a robot object with given parameters
        public Robot(double speed, double batteryPercentage, int locationX, int locationY)
        {
            this.speed = speed;
            this.batteryPercentage = batteryPercentage;
            this.totalWeightOfItemsCarried = 0;
            this.locationX = locationX;
            this.locationY = locationY;
            this.isStocking = false;
            this.isLoading = false;

            itemsCarried = new List<Item>();
        }

        // constructor overloading: initialization of a robot object without parameters
        public Robot(Warehouse warehouse, char robotSymbol)
        {
            this.robotSymbol = robotSymbol;
            this.speed = 0;
            this.batteryPercentage = 100;
            this.totalWeightOfItemsCarried = 0;
            this.locationX = 0;
            this.locationY = 0;
            this.isStocking = false;
            this.isLoading = false;

            itemsCarried = new List<Item>();
            this.homeWarehouse = warehouse;
        }

        // getters and setters
        // For how to use them: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.getaccessors?view=net-6.0
        public double Speed { get => speed; set => speed = value; }
        public double BatteryPercentage { get => batteryPercentage; set => batteryPercentage = value; }
        public double TotalWeightOfItemsCarried { get => totalWeightOfItemsCarried; set => totalWeightOfItemsCarried = value; }
        public int LocationX { get => locationX; set => locationX = value; }
        public int LocationY { get => locationY; set => locationY = value; }
        public bool IsStocking { get => isStocking; set => isStocking = value; }
        public bool IsLoading { get => isLoading; set => isLoading = value; }

        public double carrying_capacity { get => CARRYING_CAPACITY; }

        public List<Item> getItemsCarried()
        {
            return itemsCarried;
        }

       

        public void move(int endX, int endY, WarehouseGrid<TileNode> grid, char symbol)
        {
            //Console.WriteLine("Robot {0} started", symbol);
            Warehouse.gridMuxtex.WaitOne();

            grid.GetGridObject(locationX, locationY).setGridSymbol(symbol);
            Warehouse.gridMuxtex.ReleaseMutex();
            List<TileNode> path;
            do
            {


                Thread.Sleep(TIME_TO_MOVE);

                Warehouse.gridMuxtex.WaitOne();
                
                Console.WriteLine("Robot {0} moves", symbol);
                PathFinding pathFinding = new PathFinding(grid);
                path = pathFinding.FindPath(locationX, locationY, endX, endY);
                if (path != null)
                {
                    //free the current position
                    grid.GetGridObject(locationX, locationY).isFree = true;
                    //reset the layout symbol back to isle or loading bay
                    if (endX == locationX && endY == locationY && grid.GetGridObject(locationX, locationY).tileType == 1)
                    {

                    }
                    else
                    {
                        grid.GetGridObject(locationX, locationY).setGridSymbol(grid.GetGridObject(locationX, locationY).tileType == 0 ? '.' : 'D');
                    }
                        if (path.Count > 1)
                    {

                        path[1].isFree = false;
                        locationX = path[1].xLocation;
                        locationY = path[1].yLocation;

                        //for debugging
                        path[1].setGridSymbol(symbol);
                    }
                }
                grid.printGrid();
                Warehouse.gridMuxtex.ReleaseMutex();
                
            } while (path.Count > 2); //only one item when arrived at final destination

            

        }

        public void addItemToItemsCarried(Item item) //and removes from shelf
        {
            
            Thread.Sleep(TIME_TO_ADD_ITEM);
            TileNode tileWithItem = homeWarehouse.itemLocation[item.ItemID][0];
            itemsCarried.Add(item);
            totalWeightOfItemsCarried += item.ItemWeight;

            //remove from shelf item list
            //no mutex since impossible for two robots at same shelf
            tileWithItem.rack.Shelves[item.ShelfNum].removeItemfromShelf(item);

            //remove from itemLocation list 
            Warehouse.itemLocationMutex.WaitOne();
            homeWarehouse.itemLocation[item.ItemID].RemoveAt(0);
            homeWarehouse.itemQuantity[item.ItemID]--;
            Warehouse.itemLocationMutex.ReleaseMutex();
            
        }

       
        public bool goToLocation(TileNode location)
        {
            if (location.tileType == 2)
            {
                
                move(location.xLocation - 1, location.yLocation, homeWarehouse.grid, robotSymbol);
                return true;
            }
            else if (location.tileType == 3)
            {
                move(location.xLocation + 1, location.yLocation, homeWarehouse.grid, robotSymbol);
                return true;
            }
            else
                return false;
        }

        public void goToTruck(TileNode truckDockedLocation)
        {
            move(truckDockedLocation.xLocation, truckDockedLocation.yLocation, homeWarehouse.grid, robotSymbol);
        }

        public void processOrder(Order order)
        {
            this.isLoading = true;

            foreach (Item item in order.ItemList)
            {
                TileNode itemTile = homeWarehouse.itemLocation[item.ItemID][0];
                if (!goToLocation(itemTile))
                {
                    Console.WriteLine("item {0} not found", item.ItemName);
                    continue;
                }
                addItemToItemsCarried(item); //and removes from shelf
            }

            goToTruck(order.assignedTruck.dockedLocation);
            order.assignedTruck.loadOrder(order);
            itemsCarried.Clear();

            //go back to waiting spot
            move(0, 0, homeWarehouse.grid, robotSymbol);
            homeWarehouse.grid.GetGridObject(0, 0).isFree = true;
            homeWarehouse.grid.GetGridObject(locationX, locationY).setGridSymbol('.');

            isLoading = false;
        }

        public void processRestock(Truck truck)

        {
            this.isStocking = true;
            TileNode robotLocation = homeWarehouse.grid.GetGridObject(LocationX, LocationY);

            Item itemToRestock = new Item();
            goToTruck(truck.dockedLocation);

            while (truck.checkNextItemWeight() != -1 && this.totalWeightOfItemsCarried + truck.checkNextItemWeight() <= CARRYING_CAPACITY)
            {
                itemToRestock = truck.unloadItem();
                itemsCarried.Add(itemToRestock);
                totalWeightOfItemsCarried += itemToRestock.ItemWeight;
                Thread.Sleep(TIME_TO_ADD_ITEM);
            }


            List<Item> itemsRemoved = new List<Item>();
            foreach (Item item in this.itemsCarried)
            {
                Shelf currentShelf = null;
                TileNode currentRack = null;
                Dictionary<TileNode, int> shelfToRestock;
                do
                {
                    Warehouse.itemLocationMutex.WaitOne();
                    shelfToRestock = homeWarehouse.findAvailableShelf(robotLocation, item.ItemWeight);
                    Warehouse.itemLocationMutex.ReleaseMutex();
                } while (shelfToRestock == null); //if no available shelf then keep waiting until there is

                

                foreach( KeyValuePair<TileNode, int> kvp in shelfToRestock) { //i dont know how to do this wihtout foreach lol
                     currentShelf = kvp.Key.rack.Shelves[kvp.Value];
                    currentRack = kvp.Key;
                    
                }
                goToLocation(currentRack);
                Warehouse.itemLocationMutex.WaitOne();
                homeWarehouse.itemQuantity[item.ItemID]++;
                item.willBeStocked = false;
                if (homeWarehouse.itemLocation.ContainsKey(item.ItemID))
                {
                    homeWarehouse.itemLocation[item.ItemID].Add(currentRack);
                }
                else
                {
                   
                    List<TileNode> itemLocations = new List<TileNode>();
                    homeWarehouse.itemLocation.Add(item.ItemID, itemLocations);

                }
                

                currentShelf.Items.Add(item);
                Warehouse.itemLocationMutex.ReleaseMutex();

                itemsRemoved.Add(item);
                
            }
            foreach(Item item in itemsRemoved)
            {
                this.itemsCarried.Remove(item);
                this.totalWeightOfItemsCarried -= item.ItemWeight;
            }

            //go back to waiting spot
            move(0, 0, homeWarehouse.grid, robotSymbol);
            homeWarehouse.grid.GetGridObject(0, 0).isFree = true;
            homeWarehouse.grid.GetGridObject(locationX, locationY).setGridSymbol('.');
            homeWarehouse.restockDone = true;
            this.isStocking = false;
        }
    }
}
