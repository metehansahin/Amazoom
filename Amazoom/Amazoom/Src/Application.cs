using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DataLibrary;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace Amazoom
{
    public partial class ManagerWindow : Form
    {
        Warehouse warehouse = new Warehouse();
        string warehouseID = "warehouse1";
        MemoryMappedFile MMF;

        public ManagerWindow(string[] args)
        {
            InitializeComponent();

            addOneRobot(); // Add one robot by default

            if (args.Length > 0)
            {
                warehouseID = args[0];
            }

            MMF = MemoryMappedFile.OpenExisting(warehouseID);
            //MMF = MemoryMappedFile.OpenExisting("warehouse1");
        }

        private void ManagerWindow_Load(object sender, EventArgs e)
        {

        }

        private void updateRobotList()
        {
            robotList.Text = "";

            foreach (Robot robot in warehouse.robots)
            {
                robotList.Text += "X: " + robot.LocationX + " Y: " + robot.LocationY + " ProcessingOrder: " + robot.IsLoading + Environment.NewLine;
            }
        }

        private void warehouseStart_Click(object sender, EventArgs e)
        {
            /* 
             * Start warehouse command is sent, should start a new thread with that manages the warehouse
             * When this happens we need to prevent the button from being pressed again (this prevents mutliple instances of the warehouse).
             */
            warehouseStart.Enabled = false;
            warehouseBrowse.Enabled = false;

            string[] lines = System.IO.File.ReadAllLines(warehouseFileBox.Text);
            warehouse.grid = new WarehouseGrid<TileNode>(lines, Convert.ToInt32(numShelves.Value), (WarehouseGrid<TileNode> grid, int x, int y, int type, int shelfNum, int id) => new TileNode(grid, x, y, type, shelfNum, id));
            warehouse.loadingDockLocation = warehouse.findLoadingDockLocation(warehouse.grid);
            Thread warehouseManagerThread = new Thread(warehouse.Start);
            warehouseManagerThread.Start();

            statusText.Text = "Warehouse active";
            statusText.ForeColor = Color.Green;

        }

        private void statusText_Click(object sender, EventArgs e)
        {

        }

        private void addItem_Click(object sender, EventArgs e)
        {
        }

        private void placeOrder_Click(object sender, EventArgs e)
        {

        }

        private void addRobot_Click(object sender, EventArgs e)
        {
            addOneRobot();
        }

        private void addOneRobot()
        {
            char[] sym = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random rand = new Random();
            Robot newRobot = new Robot(warehouse, sym[rand.Next(0, sym.Length - 1)]);
            warehouse.robots.Add(newRobot);
            updateRobotList();
        }

        private void updateTick_Tick(object sender, EventArgs e)
        {
            updateRobotList();

            // Get database inventory
            List<DataLibrary.Models.ItemModel> items = DataLibrary.BusinessLogic.ItemProcessor.LoadItems();

            // Updates the item list
            updateItemList(items);

            if (!warehouseStart.Enabled)
            {
                Warehouse.gridMuxtex.WaitOne();
                warehouseGridText.Text = warehouse.grid.printGridString();
                Warehouse.gridMuxtex.ReleaseMutex();
            }

            Mutex mutex = Mutex.OpenExisting("mutexMMF");

            // Update MMF
            for (int i = 1; i <= warehouse.items.Count(); i++)
            {
                mutex.WaitOne();

                using (MemoryMappedViewStream stream = MMF.CreateViewStream(i*4, 4))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(warehouse.itemQuantity[i]);
                }
                mutex.ReleaseMutex();
            }

            // Place restock orders
            foreach (Item item in warehouse.items)
            {
                if (warehouse.itemQuantity[item.ItemID] < warehouse.itemMinStock[item.ItemID] && warehouse.restockDone)
                {
                   warehouse.restockDone = false;
                    if (warehouse.restockTruckQueue.Where(p => p.itemList.First().ItemID == item.ItemID).Count() == 0)
                    {
                        // Place an order for restock
                        int quantityToOrder = (warehouse.itemMinStock[item.ItemID]- warehouse.itemQuantity[item.ItemID])+5;
                        List<Item> itemList = new List<Item>();

                        for (int i = 0; i < quantityToOrder; i++)
                        {
                            
                            itemList.Add(item);
                        }
                        warehouse.restockTruckQueue.Enqueue(new Truck(itemList));
                    }
                }
            }

            // Check for new orders
            List<DataLibrary.Models.BundleModel> orders = DataLibrary.BusinessLogic.BundleProcessor.LoadBundles();
            foreach(DataLibrary.Models.BundleModel order in orders)
            {
                if(order.AssignedWarehouse == warehouseID)
                {
                    if (warehouse.itemQuantity[order.ItemID] >= order.Quantity)
                    {
                        Order orderObj = new Order(order.OrderID);
                        orderObj.addItemtoOrder(warehouse.items.Where(p => p.ItemID == order.ItemID).First(), order.Quantity);
                        warehouse.orders.Enqueue(orderObj);
                        DataLibrary.BusinessLogic.BundleProcessor.RemoveOrder(order.OrderID);
                    }
                }
            }
        }

        private void updateItemList(List<DataLibrary.Models.ItemModel> items)
        {
            warehouse.items.Clear();
            inventoryList.Items.Clear();
            foreach (DataLibrary.Models.ItemModel item in items)
            {
                warehouse.AddItem(item.ItemID, item.ItemName, Convert.ToInt32(item.ItemWeight));
                inventoryList.Items.Add("ID: " + item.ItemID + " Name: " + item.ItemName + " Stock: " + warehouse.itemQuantity[item.ItemID] + " Min Stock: " + warehouse.itemMinStock[item.ItemID]);
            }
        }

        private void warehouseBrowse_Click(object sender, EventArgs e)
        {
            // Show file browse box
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Sets warehouse file box to location of selected file
                warehouseFileBox.Text = fileDialog.FileName;
            }
        }

        private void setMin_Click(object sender, EventArgs e)
        {
            List<DataLibrary.Models.ItemModel> items = DataLibrary.BusinessLogic.ItemProcessor.LoadItems();
            warehouse.itemMinStock[Convert.ToInt32(minIDEntry.Value)] = Convert.ToInt32(minStockEntry.Value);
            updateItemList(items);
        }

        private void warehouseGridText_TextChanged(object sender, EventArgs e)
        {

        }

        private void warehouseFileBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}