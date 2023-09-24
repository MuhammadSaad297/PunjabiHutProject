using PunjabiHutProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PunjabiHutProject.Controllers
{
    public class PurchaseController : Controller
    {
        Model1 db = new Model1();

        public ActionResult Purchase(int? id)
        {
            ShopModel s = new  ShopModel();
                s.Cat = db.Categories.ToList();
            if (id == null )
            s.Pro = db.Products.ToList();
            else
                s.Pro = db.Products.Where(p => p.CATEGORY_FID == id).ToList();


            return View(s) ;

        }

        public ActionResult Cart()
        {
            return View();

        }


        public ActionResult AddToCart(int id)
        {
            List<Product> List;
            if (Session["myCart"] == null)
            { List = new List<Product>(); }
            else
            { List = (List<Product>)Session["myCart"]; }
            List.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
            
            List[List.Count - 1].PRO_QUANTITY = 1;
            Session["myCart"] = List;
            return RedirectToAction("Cart");

        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> List = (List<Product>)Session["myCart"];
            
            List[RowNo].PRO_QUANTITY -=2;
            Session["myCart"] = List;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusFromCart(int RowNo)
        {
            List<Product> List = (List<Product>)Session["myCart"];
            
            List[RowNo].PRO_QUANTITY += 3;
            Session["myCart"] = List;
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> List = (List<Product>)Session["myCart"];
            
            List.RemoveAt(RowNo);
            Session["myCart"] = List; 
            return RedirectToAction("Cart");
        }

        public ActionResult PurchaseNow(Order o)
        {

            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_STATUS = "Paid";
            o.ORDER_TYPE = "Purchase";

            Session["Order"] = o;
            return RedirectToAction("OrderBooked");
        }

        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];
          

            //OrderDetail Save In DataBase.

            db.Orders.Add(o);
            db.SaveChanges();

            List<Product> p = (List<Product>)Session["myCart"];
            for (int i = 0; i<p.Count; i++)
            {

                OrderDetail od = new OrderDetail();

                int orderID = db.Orders.Max(x => x.ORDER_ID);
                od.ORDER_FID = orderID;
                od.PRODUCT_FID = p[i].PRODUCT_ID;
                od.QUANTITY = p[i].PRO_QUANTITY;
                od.PURCHASE_PRICE = p[i].PRODUCT_PURCHASEPRICE;
                od.SALE_PRICE = p[i].PRODUCT_SALEPRICE;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }
            return View();

        }

    }

}