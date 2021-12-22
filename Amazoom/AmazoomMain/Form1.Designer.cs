
namespace AmazoomMain
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.warehouse3 = new System.Windows.Forms.Button();
            this.warehouse2 = new System.Windows.Forms.Button();
            this.warehouse1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.webserver = new System.Windows.Forms.Button();
            this.addItem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.itemName = new System.Windows.Forms.TextBox();
            this.itemList = new System.Windows.Forms.ListBox();
            this.itemID = new System.Windows.Forms.NumericUpDown();
            this.itemMass = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.itemID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemMass)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Warehouse 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Warehouse 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Warehouse 3";
            // 
            // warehouse3
            // 
            this.warehouse3.Location = new System.Drawing.Point(161, 204);
            this.warehouse3.Name = "warehouse3";
            this.warehouse3.Size = new System.Drawing.Size(98, 51);
            this.warehouse3.TabIndex = 3;
            this.warehouse3.Text = "start";
            this.warehouse3.UseVisualStyleBackColor = true;
            this.warehouse3.Click += new System.EventHandler(this.warehouse3_Click);
            // 
            // warehouse2
            // 
            this.warehouse2.Location = new System.Drawing.Point(161, 108);
            this.warehouse2.Name = "warehouse2";
            this.warehouse2.Size = new System.Drawing.Size(98, 51);
            this.warehouse2.TabIndex = 4;
            this.warehouse2.Text = "start";
            this.warehouse2.UseVisualStyleBackColor = true;
            this.warehouse2.Click += new System.EventHandler(this.warehouse2_Click);
            // 
            // warehouse1
            // 
            this.warehouse1.Location = new System.Drawing.Point(161, 12);
            this.warehouse1.Name = "warehouse1";
            this.warehouse1.Size = new System.Drawing.Size(98, 51);
            this.warehouse1.TabIndex = 5;
            this.warehouse1.Text = "start";
            this.warehouse1.UseVisualStyleBackColor = true;
            this.warehouse1.Click += new System.EventHandler(this.warehouse1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Webserver";
            // 
            // webserver
            // 
            this.webserver.Location = new System.Drawing.Point(161, 300);
            this.webserver.Name = "webserver";
            this.webserver.Size = new System.Drawing.Size(98, 51);
            this.webserver.TabIndex = 7;
            this.webserver.Text = "start";
            this.webserver.UseVisualStyleBackColor = true;
            this.webserver.Click += new System.EventHandler(this.webserver_Click);
            // 
            // addItem
            // 
            this.addItem.Location = new System.Drawing.Point(747, 25);
            this.addItem.Name = "addItem";
            this.addItem.Size = new System.Drawing.Size(98, 51);
            this.addItem.TabIndex = 8;
            this.addItem.Text = "add";
            this.addItem.UseVisualStyleBackColor = true;
            this.addItem.Click += new System.EventHandler(this.addItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Name";
            // 
            // itemName
            // 
            this.itemName.Location = new System.Drawing.Point(414, 36);
            this.itemName.Name = "itemName";
            this.itemName.Size = new System.Drawing.Size(233, 29);
            this.itemName.TabIndex = 11;
            this.itemName.TextChanged += new System.EventHandler(this.itemName_TextChanged);
            // 
            // itemList
            // 
            this.itemList.FormattingEnabled = true;
            this.itemList.ItemHeight = 24;
            this.itemList.Location = new System.Drawing.Point(320, 88);
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(525, 268);
            this.itemList.TabIndex = 13;
            this.itemList.SelectedIndexChanged += new System.EventHandler(this.itemList_SelectedIndexChanged);
            // 
            // itemID
            // 
            this.itemID.Location = new System.Drawing.Point(320, 36);
            this.itemID.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.itemID.Name = "itemID";
            this.itemID.Size = new System.Drawing.Size(88, 29);
            this.itemID.TabIndex = 14;
            // 
            // itemMass
            // 
            this.itemMass.Location = new System.Drawing.Point(653, 36);
            this.itemMass.Name = "itemMass";
            this.itemMass.Size = new System.Drawing.Size(88, 29);
            this.itemMass.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(649, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "Mass";
            // 
            // tick
            // 
            this.tick.Enabled = true;
            this.tick.Interval = 500;
            this.tick.Tick += new System.EventHandler(this.tick_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.addItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 372);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.itemMass);
            this.Controls.Add(this.itemID);
            this.Controls.Add(this.itemList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.itemName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addItem);
            this.Controls.Add(this.webserver);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.warehouse1);
            this.Controls.Add(this.warehouse2);
            this.Controls.Add(this.warehouse3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Warehouse Admin";
            ((System.ComponentModel.ISupportInitialize)(this.itemID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemMass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button warehouse3;
        private System.Windows.Forms.Button warehouse2;
        private System.Windows.Forms.Button warehouse1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button webserver;
        private System.Windows.Forms.Button addItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox itemName;
        private System.Windows.Forms.ListBox itemList;
        private System.Windows.Forms.NumericUpDown itemID;
        private System.Windows.Forms.NumericUpDown itemMass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer tick;
    }
}

