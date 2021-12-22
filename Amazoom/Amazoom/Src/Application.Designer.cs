namespace Amazoom
{
    partial class ManagerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.warehouseStart = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.Label();
            this.robotList = new System.Windows.Forms.TextBox();
            this.addRobot = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.updateTick = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.warehouseFileBox = new System.Windows.Forms.TextBox();
            this.warehouseBrowse = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.numShelves = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.warehouseGridText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addTruck = new System.Windows.Forms.Button();
            this.truckList = new System.Windows.Forms.TextBox();
            this.inventoryList = new System.Windows.Forms.ListBox();
            this.textbox92 = new System.Windows.Forms.Label();
            this.minIDEntry = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.minStockEntry = new System.Windows.Forms.NumericUpDown();
            this.setMin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numShelves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minIDEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minStockEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // warehouseStart
            // 
            this.warehouseStart.Location = new System.Drawing.Point(15, 716);
            this.warehouseStart.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.warehouseStart.Name = "warehouseStart";
            this.warehouseStart.Size = new System.Drawing.Size(342, 136);
            this.warehouseStart.TabIndex = 0;
            this.warehouseStart.Text = "Start warehouse";
            this.warehouseStart.UseVisualStyleBackColor = true;
            this.warehouseStart.Click += new System.EventHandler(this.warehouseStart_Click);
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.ForeColor = System.Drawing.Color.Red;
            this.statusText.Location = new System.Drawing.Point(363, 764);
            this.statusText.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.statusText.Name = "statusText";
            this.statusText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusText.Size = new System.Drawing.Size(344, 36);
            this.statusText.TabIndex = 1;
            this.statusText.Text = "Warehouse not activated";
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.statusText.Click += new System.EventHandler(this.statusText_Click);
            // 
            // robotList
            // 
            this.robotList.Location = new System.Drawing.Point(15, 142);
            this.robotList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.robotList.Multiline = true;
            this.robotList.Name = "robotList";
            this.robotList.ReadOnly = true;
            this.robotList.Size = new System.Drawing.Size(780, 204);
            this.robotList.TabIndex = 10;
            // 
            // addRobot
            // 
            this.addRobot.Location = new System.Drawing.Point(15, 50);
            this.addRobot.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.addRobot.Name = "addRobot";
            this.addRobot.Size = new System.Drawing.Size(782, 81);
            this.addRobot.TabIndex = 14;
            this.addRobot.Text = "Add Robot";
            this.addRobot.UseVisualStyleBackColor = true;
            this.addRobot.Click += new System.EventHandler(this.addRobot_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(15, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Robots";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateTick
            // 
            this.updateTick.Enabled = true;
            this.updateTick.Interval = 500;
            this.updateTick.Tick += new System.EventHandler(this.updateTick_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(821, 94);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 24);
            this.label10.TabIndex = 25;
            this.label10.Text = "Warehouse File";
            // 
            // warehouseFileBox
            // 
            this.warehouseFileBox.Location = new System.Drawing.Point(1084, 88);
            this.warehouseFileBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.warehouseFileBox.Name = "warehouseFileBox";
            this.warehouseFileBox.ReadOnly = true;
            this.warehouseFileBox.Size = new System.Drawing.Size(366, 29);
            this.warehouseFileBox.TabIndex = 26;
            this.warehouseFileBox.Text = "C:\\Users\\Lucy Hua\\Documents\\School\\Year 3\\CPEN 333\\Project\\CPEN333Project\\warehou" +
    "seLayout.txt";
            this.warehouseFileBox.TextChanged += new System.EventHandler(this.warehouseFileBox_TextChanged);
            // 
            // warehouseBrowse
            // 
            this.warehouseBrowse.AutoSize = true;
            this.warehouseBrowse.Location = new System.Drawing.Point(1462, 76);
            this.warehouseBrowse.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.warehouseBrowse.Name = "warehouseBrowse";
            this.warehouseBrowse.Size = new System.Drawing.Size(148, 58);
            this.warehouseBrowse.TabIndex = 28;
            this.warehouseBrowse.Text = "Browse";
            this.warehouseBrowse.UseVisualStyleBackColor = true;
            this.warehouseBrowse.Click += new System.EventHandler(this.warehouseBrowse_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "txt";
            this.fileDialog.FileName = "file";
            this.fileDialog.Filter = "Text files (*.txt)|*.txt";
            // 
            // numShelves
            // 
            this.numShelves.Location = new System.Drawing.Point(1084, 46);
            this.numShelves.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numShelves.Name = "numShelves";
            this.numShelves.Size = new System.Drawing.Size(367, 29);
            this.numShelves.TabIndex = 30;
            this.numShelves.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(821, 50);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 24);
            this.label11.TabIndex = 31;
            this.label11.Text = "Number of shelves";
            // 
            // warehouseGridText
            // 
            this.warehouseGridText.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warehouseGridText.Location = new System.Drawing.Point(825, 147);
            this.warehouseGridText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.warehouseGridText.Multiline = true;
            this.warehouseGridText.Name = "warehouseGridText";
            this.warehouseGridText.ReadOnly = true;
            this.warehouseGridText.Size = new System.Drawing.Size(780, 282);
            this.warehouseGridText.TabIndex = 32;
            this.warehouseGridText.TextChanged += new System.EventHandler(this.warehouseGridText_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(813, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "Warehouse";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(11, 370);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 34;
            this.label2.Text = "Delivery Truck";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addTruck
            // 
            this.addTruck.Location = new System.Drawing.Point(15, 410);
            this.addTruck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.addTruck.Name = "addTruck";
            this.addTruck.Size = new System.Drawing.Size(782, 81);
            this.addTruck.TabIndex = 36;
            this.addTruck.Text = "Add Truck";
            this.addTruck.UseVisualStyleBackColor = true;
            // 
            // truckList
            // 
            this.truckList.Location = new System.Drawing.Point(15, 502);
            this.truckList.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.truckList.Multiline = true;
            this.truckList.Name = "truckList";
            this.truckList.ReadOnly = true;
            this.truckList.Size = new System.Drawing.Size(780, 196);
            this.truckList.TabIndex = 35;
            // 
            // inventoryList
            // 
            this.inventoryList.ItemHeight = 24;
            this.inventoryList.Location = new System.Drawing.Point(825, 477);
            this.inventoryList.Name = "inventoryList";
            this.inventoryList.Size = new System.Drawing.Size(785, 172);
            this.inventoryList.TabIndex = 37;
            // 
            // textbox92
            // 
            this.textbox92.AutoSize = true;
            this.textbox92.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox92.ForeColor = System.Drawing.Color.Blue;
            this.textbox92.Location = new System.Drawing.Point(821, 441);
            this.textbox92.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textbox92.Name = "textbox92";
            this.textbox92.Size = new System.Drawing.Size(86, 24);
            this.textbox92.TabIndex = 38;
            this.textbox92.Text = "Inventory";
            this.textbox92.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minIDEntry
            // 
            this.minIDEntry.Location = new System.Drawing.Point(826, 682);
            this.minIDEntry.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.minIDEntry.Name = "minIDEntry";
            this.minIDEntry.Size = new System.Drawing.Size(65, 29);
            this.minIDEntry.TabIndex = 39;
            this.minIDEntry.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(824, 652);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 24);
            this.label3.TabIndex = 40;
            this.label3.Text = "ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(896, 652);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 24);
            this.label5.TabIndex = 42;
            this.label5.Text = "Min Stock";
            // 
            // minStockEntry
            // 
            this.minStockEntry.Location = new System.Drawing.Point(901, 682);
            this.minStockEntry.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.minStockEntry.Name = "minStockEntry";
            this.minStockEntry.Size = new System.Drawing.Size(136, 29);
            this.minStockEntry.TabIndex = 41;
            this.minStockEntry.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // setMin
            // 
            this.setMin.AutoSize = true;
            this.setMin.Location = new System.Drawing.Point(1059, 668);
            this.setMin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.setMin.Name = "setMin";
            this.setMin.Size = new System.Drawing.Size(148, 58);
            this.setMin.TabIndex = 43;
            this.setMin.Text = "Set Min";
            this.setMin.UseVisualStyleBackColor = true;
            this.setMin.Click += new System.EventHandler(this.setMin_Click);
            // 
            // ManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1665, 870);
            this.Controls.Add(this.setMin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minStockEntry);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minIDEntry);
            this.Controls.Add(this.textbox92);
            this.Controls.Add(this.inventoryList);
            this.Controls.Add(this.addTruck);
            this.Controls.Add(this.truckList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.warehouseGridText);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numShelves);
            this.Controls.Add(this.warehouseBrowse);
            this.Controls.Add(this.warehouseFileBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addRobot);
            this.Controls.Add(this.robotList);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.warehouseStart);
            this.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.Name = "ManagerWindow";
            this.Text = "Warehouse Manager";
            this.Load += new System.EventHandler(this.ManagerWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numShelves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minIDEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minStockEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button warehouseStart;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.TextBox robotList;
        private System.Windows.Forms.Button addRobot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer updateTick;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox warehouseFileBox;
        private System.Windows.Forms.Button warehouseBrowse;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.NumericUpDown numShelves;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox warehouseGridText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addTruck;
        private System.Windows.Forms.TextBox truckList;
        private System.Windows.Forms.Label textbox92;
        private System.Windows.Forms.ListBox inventoryList;
        private System.Windows.Forms.NumericUpDown minIDEntry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minStockEntry;
        private System.Windows.Forms.Button setMin;
    }
}