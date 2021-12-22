using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazooomMVCDotNet.Models
{
    public class Bundle
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
    }
}