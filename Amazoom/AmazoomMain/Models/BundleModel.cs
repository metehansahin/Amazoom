using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazoomMain
{
    public class BundleModel
    {
        public int Id { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public string AssignedWarehouse { get; set; }
    }
}
