using AmazooomMVCDotNet.Models;
using static DataLibrary.BusinessLogic.ItemProcessor;
using static DataLibrary.BusinessLogic.BundleProcessor;
using DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazooomMVCDotNet.Controllers
{
    public class HomeController : Controller
    {
        List<Bundle> shoppingCart = new List<Bundle>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Amazooom, the world's #1 retail and delivery service!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "To contact, call 911";

            return View();
        }

        public ActionResult ViewStock()
        {
            ViewBag.Message = "All Products";

            var data = LoadItems();
            List<ItemModel> items = new List<ItemModel>();

            foreach (var row in data)
            {
                items.Add(new ItemModel
                {
                    ItemID = row.ItemID,
                    ItemWeight = row.ItemWeight,
                    ItemVolume = row.ItemVolume,
                    ItemName = row.ItemName,
                    Stock = row.Stock
                });
            }
            return View(items);
        }

        public ActionResult AddStock()
        {
            ViewBag.Message = "Add Item";

            return View();
        }
        [HttpPost]
        public ActionResult AddStock(ItemModel model)
        {
            if(ModelState.IsValid)
            {
                int recordsCreated = CreateItem(model.ItemID, 
                    model.ItemWeight, 
                    model.ItemVolume, 
                    model.ItemName, 
                    model.Stock);
                return RedirectToAction("ViewStock"); 
            }

            return View();
        }
        public ActionResult AddToCart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(Bundle model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateBundle(model.ItemID, model.ItemName,
                    model.Quantity, model.OrderID);
                return RedirectToAction("AddToCart");
            }

            return View();
        }

        public ActionResult ViewCart()
        {
            ViewBag.Message = "Checkout";

            var data = LoadBundles();
            List<Bundle> bundles= new List<Bundle>();

            foreach (var row in data)
            {
                bundles.Add(new Bundle
                {
                    ItemID = row.ItemID,
                    ItemName = row.ItemName,
                    Quantity = row.Quantity,
                    OrderID = row.OrderID
                }) ;
            }
            return View(bundles);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}