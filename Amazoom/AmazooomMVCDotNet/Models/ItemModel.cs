using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazooomMVCDotNet.Models
{
    public class ItemModel
    {
        public int ItemID { get; set; }
        public double ItemWeight { get; set; }
        public double ItemVolume { get; set; }
        public string ItemName { get; set; }
        public int Stock { get; set; }
    }
}