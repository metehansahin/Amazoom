using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Amazoom
{
    public class Warehouse
    {
        // Lists that contain all entities in the warehouse
        public List<Item> items = new List<Item>();
        public Dictionary<int, int> itemQuantity = new Dictionary<int, int>(); // Updated in order 
        public Dictionary<int, int> itemMinStock = new Dictionary<int, int>(); // Updated in form application
        public Dictionary<int, List<TileNode>> itemLocation = new Dictionary<int, List<TileNode>>(); // Updated in robot 
        public List<TileNode> loadingDockLocation = new List<TileNode>();

        public List<Robot> robots = new List<Robot>();
        private List<Truck> dockedTrucks = new List<Truck>();
        public Queue<Truck> restockTruckQueue = new Queue<Truck>();
        public Queue<Truck> deliveryTruckQueue = new Queue<Truck>();
        public Queue<Truck> deliveryTruckonRoad = new Queue<Truck>(); //trucks currently "on the road"
        public Queue<Order> orders = new Queue<Order>();
        public Queue<Order> waitlistedOrders = new Queue<Order>();
        public WarehouseGrid<TileNode> grid;

        public static Mutex itemLocationMutex = new Mutex();
        public static Mutex gridMuxtex = new Mutex();
        public static Mutex truckMut = new Mutex();

        public int numShelvesPerRack = 1; //TO CHANGE

        public bool restockDone = true;

        /*
         * Warehouse function is called when a new warehouse is created
         * This method sets up any inital parameters
         */
        public Warehouse()
        {
           
        }

        /*
         * This is the main method that gets called when the warehouse starts
         * Start is called by a thread and it will handel all the scheduling in the warehouse
         */
        public void Start()
        {
            // Add some trucks
            for(int i = 0; i < 3; i++)
            {
                Truck truck = new Truck();
                deliveryTruckQueue.Enqueue(truck);
            }
           
            Thread deliveryTruckControl = new Thread(() => this.deliveryTruckSimulator());
            deliveryTruckControl.Start();

            Thread loadingDockControl = new Thread(() => this.loadingDockControl());
            loadingDockControl.Start();
            Console.WriteLine("started loading dock control thread");

            while (true)
            {
                bool waitingForRobot = true;
                Robot availableRobot = robots.First();
                // Keeps repeating until there is a free robot
                while (waitingForRobot)
                {
                    foreach (Robot robot in robots)
                    {
                        if (!(robot.IsLoading || robot.IsStocking))
                        {
                            waitingForRobot = false;
                            availableRobot = robot;
                            break;
                        }
                    }
                }
                Console.WriteLine("Found robot");

                //check for restock truck (higher priority)
                if (assignToEligibleRestockingTruck(availableRobot, dockedTrucks))
                {
                    //above function calls restocking func
                }
                // Check for order in tempOrder (higher priority than in main order queue)
                else if (waitlistedOrders.Count() > 0)
                {
                    Order currentOrder = waitlistedOrders.Peek();
                    //check for avilable delivery truck
                    if (assignToEligibleDeliveryTruck(currentOrder, dockedTrucks))
                    {
                        //check if a single robot can handle current order
                        if (currentOrder.OrderWeight > availableRobot.carrying_capacity)
                        {
                            //it cant
                            
                            //replace temp order with copied order
                            currentOrder = copyOrder(currentOrder);
                            //assign current order to robot
                            Thread robotThreadDelivery = new Thread(() => availableRobot.processOrder(currentOrder));
                            robotThreadDelivery.Start();

                        }
                        else
                        {
                            //it can 
                            Thread robotThreadDelivery = new Thread(() => availableRobot.processOrder(waitlistedOrders.Dequeue()));
                            robotThreadDelivery.Start();


                        }

                    }

                    //we are assuming wailtisted orders are just exceptionally large order. 
                    //just to prevent deadlock
                    else if (waitlistedOrders.Count() > 4)
                    {
                        Truck truckToLeave = findFullestTruck(dockedTrucks);


                        while (truckToLeave.orderList != truckToLeave.currentOrderList)
                        {
                            ////will have to wait until all the assigned orders are loaded onto truck before force to leave 
                            //not best implementation
                            //DOES THIS CREATE DEADLOCK?
                        }

                        truckToLeave.isReady = true;
                        

                    }
                }
                else if (orders.Count() > 0)
                {
                    Order currentOrder = orders.Peek();
                    //check for avilable delivery truck
                    if (assignToEligibleDeliveryTruck(currentOrder, dockedTrucks))
                    {
                        //check if a single robot can handle current order
                        if (currentOrder.OrderWeight > availableRobot.carrying_capacity)
                        {
                            //no it cant

                            //replace temp order with copied order
                            currentOrder = copyOrder(currentOrder);
                            //assign current order to robot
                            Thread robotThreadDelivery = new Thread(() => availableRobot.processOrder(currentOrder));
                            robotThreadDelivery.Start();

                        }
                        else
                        {
                            //yes it can 
                            currentOrder = orders.Dequeue();
                            Thread robotThreadDelivery = new Thread(() => availableRobot.processOrder(currentOrder));
                            robotThreadDelivery.Start();


                        }

                    }
                    //no available truck. Truck closest to threshold value will be forced to leave. Current order is added to waitlisted queue
                    else
                    {
                        Truck truckToLeave = findFullestTruck(dockedTrucks);


                        while (truckToLeave.orderList != truckToLeave.currentOrderList)
                        {
                            ////will have to wait until all the assigned orders are loaded onto truck before force to leave 
                            //not best implementation
                            //DOES THIS CREATE DEADLOCK?
                        }

                        truckToLeave.isReady = true;
                        waitlistedOrders.Enqueue(orders.Dequeue());
                    }
                }

                /*while(availableRobot.LocationX == 0 & availableRobot.LocationY == 0)
                {
                        
                }  */  
            }
        }

        public void AddItem(int itemID, string itemName)
        {
            Item item = new Item(0, itemName, itemID, 0);
            if (items.Where(p => p.ItemID == itemID).Count() != 0)
            {
                throw new Exception("Item already exists");
            }
            else
            {
                // Adds item to list and sets quantity to zero
                items.Add(item);
                itemQuantity.Add(itemID, 0);
                itemLocation.Add(itemID, new List<TileNode>());
            }
        }

        public void AddItem(int itemID, string itemName, int itemWeight)
        {
            Item item = new Item(itemWeight, itemName, itemID, 0);
            if (items.Where(p => p.ItemID == itemID).Count() != 0)
            {
                throw new Exception("Item already exists");
            }
            else
            {
                // Adds item to list and sets quantity to zero
                items.Add(item);
                if (!itemQuantity.ContainsKey(itemID))
                {
                    itemQuantity.Add(itemID, 0);
                    itemLocation.Add(itemID, new List<TileNode>());
                    itemMinStock.Add(itemID, 0); // Sets min stock to 0
                }
            }
        }

        
  public bool assignToEligibleRestockingTruck(Robot robot, List<Truck> dockedTrucks)
        {
            truckMut.WaitOne();
            foreach (Truck truck in dockedTrucks)
            {
                //check if restocking truck
                if (truck.truckType == 1)
                {
                    //check if truck is still awaiting robots
                    if (!truck.hasEnoughAssignedRobots)
                    {
                        //assign robot to unload restocking truck
                        Thread robotThreadRestock = new Thread(() => robot.processRestock(truck));
                        robotThreadRestock.Start();
                      
                        truck.numAssignedRobots++;
                        //update truck status if applicable
                        if (truck.numAssignedRobots * robot.carrying_capacity >= truck.itemListWeight)
                        {
                            truck.hasEnoughAssignedRobots = true;
                        }
                        truckMut.ReleaseMutex();
                        return true;
                    }
                    
                }
            }
            truckMut.ReleaseMutex();
            return false;
        }
        public Truck findFullestTruck (List<Truck> dockedTrucks)
        {
            Truck fullestTruck = new Truck();
            double capacity = double.MaxValue;
            foreach(Truck truck in dockedTrucks)
            {
                if(truck.truckType == 0)
                {
                    if(truck.checkWeightUntilMax() < capacity)
                    {
                        capacity = truck.checkWeightUntilMax();
                        fullestTruck = truck;

                    }
                }
            }
            return fullestTruck;
        }
        public bool assignToEligibleDeliveryTruck(Order order, List<Truck> dockedTrucks)
        {
            bool foundEligibleTruck = false;
            int counter = 0;
            Random rand = new Random();
            int index = rand.Next(0, dockedTrucks.Count - 1);
            
            while(!foundEligibleTruck && counter < dockedTrucks.Count)
            {
                //check if delivery truck
                if (dockedTrucks[index].truckType == 0)
                {
                    if (dockedTrucks[index].checkWeightUntilMax() >= order.OrderWeight)
                    {
                        dockedTrucks[index].assignOrder(order);
                        order.assignedTruck = dockedTrucks[index];
                        foundEligibleTruck = true;
                    }
                    index = (index + 1) % dockedTrucks.Count;
                    counter++;
                }
            }

            return foundEligibleTruck;
        }

        public void deliveryTruckSimulator()
        {
            while (true)
            {
                //half a minute. simulate the truck going out for delivery
                Thread.Sleep(30000);
                //arrived back at warehouse and waiting in line now
                if(deliveryTruckonRoad.Count > 0)
                {
                    deliveryTruckQueue.Enqueue(deliveryTruckonRoad.Dequeue());
                }
                
            }
        }

        public void loadingDockControl()
        {
            TileNode freeDock; 
            while(true)
            {
                List<Truck> truckToLeave = new List<Truck>();
                foreach(Truck truck in dockedTrucks)
                {
                    if (truck.isReady)
                    {
                        truck.dockedLocation.isFree = true;
                        truckToLeave.Add(truck);
                        truck.resetTruck();
                        if(truck.truckType == 0)
                        {
                            deliveryTruckonRoad.Enqueue(truck);
                            
                        }
                        
                    }
                }
                foreach(Truck truck in truckToLeave)
                {
                    truckMut.WaitOne();
                    dockedTrucks.Remove(truck);
                    truckMut.ReleaseMutex();
                }


                freeDock = findFreeDock(loadingDockLocation);
                if(freeDock != null)
                {
                    if (restockTruckQueue.Count > 0)
                    {
                        Truck dockingTruck = restockTruckQueue.Dequeue();
                        truckMut.WaitOne();
                        dockedTrucks.Add(dockingTruck);
                        truckMut.ReleaseMutex();
                        dockingTruck.assignDockLocation(freeDock);
                        freeDock.isFree = false;
                    }
                    else if (deliveryTruckQueue.Count > 0)
                    {
                        Truck dockingTruck = deliveryTruckQueue.Dequeue();
                        truckMut.WaitOne();
                        dockedTrucks.Add(dockingTruck);
                        truckMut.ReleaseMutex();
                        dockingTruck.assignDockLocation(freeDock);
                        freeDock.isFree = false;
                    }

                }

            }
        }
        public Dictionary<TileNode, int> findAvailableShelf(TileNode robotLocation, double itemWeight)
        {
            
            Dictionary<TileNode, int> closestAvailableShelf = new Dictionary<TileNode, int>();
            int distance;
            int shortestDistance = int.MaxValue;
            PathFinding path = new PathFinding(grid);
            TileNode closestTile = null;
            int closestShelfNum=0;

           
            foreach(TileNode rackTile in grid.listOfRacks)
            {
                Rack rack = rackTile.rack;
                for (int shelfNum = 0; shelfNum < rack.Shelf_levels; shelfNum++)
                {
                    //find all shelves that have the capacity to hold item
                    if (itemWeight + rack.Shelves[shelfNum].CurrentWeight <= rack.Shelves[shelfNum].Weight_capacity)
                    {
                        //find available shelf that is closest to robot
                        distance = path.CalculateDistanceCost(robotLocation, rackTile);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            closestTile = rackTile;
                            closestShelfNum = shelfNum;
                        }
                    }
                }
            }
            closestAvailableShelf.Add(closestTile, closestShelfNum);
            return closestAvailableShelf;
        }
        //returns new order with as many items as possible under robot capacity
        //the above items are removed from original order
        public Order copyOrder(Order order)
        {
            Robot tempRobot = new Robot(this, 'x'); //only used to get carrying capacity
            double weightCapacity = tempRobot.carrying_capacity;
            int counter = order.ItemList.Count;
            Order newOrder = new Order();
            newOrder.OrderWeight = order.OrderWeight;
            newOrder.OrderNum = order.OrderNum;
            newOrder.assignedTruck = order.assignedTruck;
            
            while(newOrder.OrderWeight <= weightCapacity || counter > 0 )
            {
                newOrder.addItemtoOrder(order.ItemList[counter], 1);
                order.ItemList.RemoveAt(counter);
                counter--;
            }
            return newOrder;

        }

        public TileNode findFreeDock(List<TileNode> listOfLoadingDocks)
        {
            foreach(TileNode dock in listOfLoadingDocks)
            {
                if (dock.loadingDock.getStatus() == true) //true = is free
                {
                    return dock;
                }
            }

            return null;
        }
        

        public List<TileNode> findLoadingDockLocation(WarehouseGrid<TileNode> grid)
        {
            List<TileNode> locationList = new List<TileNode>();
            //loading dock can only be at edge of grid
            for (int i = 0; i < grid.GetNumRows(); i++)
            {
                TileNode currentTile = grid.GetGridObject(0, i);//top edge
                if (currentTile.tileType == 1) 
                {
                    locationList.Add(currentTile);
                }
                currentTile = grid.GetGridObject(grid.GetNumCols()-1, i); // bottom edge
                if (currentTile.tileType == 1) 
                {
                    locationList.Add(currentTile);
                }
            }
            for (int i = 0; i < grid.GetNumCols(); i++)
            {
                TileNode currentTile = grid.GetGridObject(i, 0);  //left edge
                if (currentTile.tileType == 1)
                {
                    locationList.Add(currentTile);
                }
                currentTile = grid.GetGridObject(i, grid.GetNumRows() - 1); // right edge
                if (currentTile.tileType == 1) //top edge
                {
                    locationList.Add(currentTile);
                }
            }
            return locationList;
        }
    }
}
