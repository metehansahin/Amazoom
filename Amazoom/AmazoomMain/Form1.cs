using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace AmazoomMain
{
    public partial class Form1 : Form
    {
        int itemCount = 0;
        List<WarehouseProcess> warehouses = new List<WarehouseProcess>();
        Mutex mutexMMF = new Mutex(false, "mutexMMF");
        Dictionary<int, int> totalStock = new Dictionary<int, int>();

        public Form1()
        {
            InitializeComponent();

            // Grab items from database
            List<ItemModel> items = ItemProcessor.LoadItems();
            int maxID = 0;
            foreach(ItemModel item in items)
            {
                itemList.Items.Add(item.ItemID + " - " + item.ItemName);
                if (item.Id > maxID)
                    maxID = item.ItemID;
            }
            itemID.Value = maxID+1;
            itemCount = items.Count;

            warehouses.Add(new WarehouseProcess("warehouse1", warehouse1));
            warehouses.Add(new WarehouseProcess("warehouse2", warehouse2));
            warehouses.Add(new WarehouseProcess("warehouse3", warehouse3));
        }

        private void tick_Tick(object sender, EventArgs e)
        {
            // Update stock from warehouses
            for(int i = 1; i <= itemCount; i++)
            {
                int itemStock = 0;

                // Cheack each warehouse
                foreach(WarehouseProcess warehouse in warehouses)
                {
                    // Check for a warehouse that has started
                    // If the warehouse has not been started there will be no MMF
                    if(warehouse.started == true)
                    {
                        // Start mutex and read from MMF
                        mutexMMF.WaitOne();

                        // Read 1 byte at the the offset
                        using (var stream = warehouse.MMF.CreateViewStream(i*4, 4))
                        {
                            // Store item quantity to warehouse
                            int itemAmount = stream.ReadByte();
                            warehouse.itemQuantity[i] = itemAmount;
                            itemStock += itemAmount;
                        }

                        // Release
                        mutexMMF.ReleaseMutex();
                    }

                    // Update database
                    ItemProcessor.UpdateItemStock(i, itemStock);
                }
            }

            // Process any orders
        }

        private void warehouse1_Click(object sender, EventArgs e)
        {
            warehouses.Find(p => p.name == "warehouse1").spawnProcess();
        }

        private void warehouse2_Click(object sender, EventArgs e)
        {
            warehouses.Find(p => p.name == "warehouse2").spawnProcess();
        }

        private void warehouse3_Click(object sender, EventArgs e)
        {
            warehouses.Find(p => p.name == "warehouse3").spawnProcess();
        }

        private void webserver_Click(object sender, EventArgs e)
        {
            webserver.Enabled = false;
            //Process web = new Process();
            //web.StartInfo.FileName = location + @"Amazoom\bin\Debug\Amazoom.exe";
            //web.StartInfo.Arguments = "";
            //web.Start();
        }

        private void addItem_Click(object sender, EventArgs e)
        {
            ItemProcessor.CreateItem(Convert.ToInt32(itemID.Value), Convert.ToInt32(itemMass.Value), 0, itemName.Text, 0);
            itemList.Items.Add(itemID.Value + " - " + itemName.Text);
            itemID.Value++;
            itemName.Text = "";
            itemCount++;
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void itemName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class WarehouseProcess
    {
        public WarehouseProcess(String name, Button start)
        {
            started = false;
            this.name = name;
            startButton = start;
            itemQuantity = new Dictionary<int, int>();
        }

        public MemoryMappedFile MMF { get; set; }
        public Process process { get; set; }
        public Button startButton { get; set; }
        public String name { get; set; }
        public bool started { get; set; }
        public Dictionary<int, int> itemQuantity { get; set; }

        public void spawnProcess()
        {
            const string location = @"C:\Users\Nicolas\Documents\School\CPEN 333\CPEN333Project\Amazoom\";

            // Create new warehouse process and disable button
            startButton.Enabled = false;
            process = new Process();
            process.StartInfo.FileName = location + @"Amazoom\bin\Debug\Amazoom.exe";
            process.StartInfo.Arguments = name;

            // Create MMF
            // Each byte of a MMF will represent the stock of the item with ID of the read location
            // Example: byte 1 = itemID, byte 5 = itemID 5 ...
            MMF = MemoryMappedFile.CreateNew(name, 1024);

            // Start process
            process.Start();

            // Set flag to true
            started = true;
        }
    }
}
