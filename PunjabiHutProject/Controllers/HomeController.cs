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
    public class HomeController : Controller
    {
        Model1 db = new Model1();

        public ActionResult IndexCustomer()
        {
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            // please count record from database
            //where email & passward is match 
            //our posted email & passward.
            //if email and passward is matched result is 1.
            //else if not matched result is 0.
            Admin a = db.Admins.Where(x => x.ADMIN_EMAIL == admin.ADMIN_EMAIL && x.ADMIN_PASSWARD == admin.ADMIN_PASSWARD).FirstOrDefault();
            if (a != null)
            {
                Session["Admin"] = a;
                return RedirectToAction("IndexAdmin", "Home");

            }
            else
            {
                ViewBag.Message = "invalid email and passward";
                return View();
            }
           


        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Shop(int? id)
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

        public ActionResult Feedback()
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

            Boolean isProductExist = false;
            foreach(var item in List)
            {
                if(id == item.PRODUCT_ID)
                {
                    isProductExist = true;
                    item.PRO_QUANTITY++;
                }
            }
            if (isProductExist == false)
            {
                List.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
                List[List.Count - 1].PRO_QUANTITY = 1;
            }
            Session["myCart"] = List;
            return RedirectToAction("Cart","Home", new { ID = id });
        }

        public ActionResult AddToExtra(int id)
        {
            List<Extra> List;
            if (Session["ExtraCart"] == null)
            { List = new List<Extra>(); }
            else
            { List = (List<Extra>)Session["ExtraCart"]; }

            Boolean isProductExist = false;
            foreach (var item in List)
            {
                if (id == item.EXTRA_ID)
                {
                    isProductExist = true;
                  
                    
                }
            }
            if (isProductExist == false)
            {
                List.Add(db.Extras.Where(p => p.EXTRA_ID == id).FirstOrDefault());
            }
            Session["ExtraCart"] = List;
            return RedirectToAction("Cart");
        }

        public ActionResult Extra(int id)
        {
            List<Extra> e = db.Extras.Where(x => x.PRODUCT_FID == id).ToList();
            return View(e);
        }
         public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> List = (List<Product>)Session["myCart"];
            
            List[RowNo].PRO_QUANTITY--;
            if (List[RowNo].PRO_QUANTITY == 0)
                List.RemoveAt(RowNo);
            Session["myCart"] = List;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusFromCart(int RowNo)
        {
            List<Product> List = (List<Product>)Session["myCart"];
            int P_ID = List[RowNo].PRODUCT_ID;
            int? available = db.OrderDetails.Where(x => x.PRODUCT_FID == P_ID).Sum(x => x.QUANTITY);
            if(available > List[RowNo].PRO_QUANTITY)
            List[RowNo].PRO_QUANTITY++;
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
        public ActionResult RemoveFromExtraCart(int ERowNo)
        {
            List<Extra> List = (List<Extra>)Session["ExtraCart"];

            List.RemoveAt(ERowNo);
            Session["ExtraCart"] = List;
            return RedirectToAction("Cart");
        }

        public ActionResult PayNow(Order o)
        {

            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_TYPE = "Sale";
            o.STATUS = "Active";
            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                o.CUSTOMER_FID =c.CUSTOMER_ID;
            }
            Session["Order"] = o;
            if(o.ORDER_STATUS== "Cash On Delivery")
            {
                return RedirectToAction("OrderBooked");
            }
            else
            {
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=zeeshan.arshad@pugc.edu.pk&item_name=PunjabiHutProducts&return=https://localhost:44364/Home/OrderBooked&amount=" + double.Parse(Session["toalAmount"].ToString()) / 160);
            }
        }

        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];
           //// Email
           // MailMessage mail = new MailMessage();
           // mail.From = new MailAddress("haseebsaroya5133@gmail.com");
           // mail.To.Add(o.ORDER_EMAIL);
           // mail.Subject = "Order Confirmation";
           // mail.Body = "<b>PunjabiHut! </b> <br/>Thanks For Your Order.";
           // mail.IsBodyHtml = true;

           // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
           // SmtpServer.Port = 587;
           // SmtpServer.Credentials = new System.Net.NetworkCredential("haseebsaroya5133@gmail.com", "H@seeb!6441");
           // SmtpServer.EnableSsl = true;
           // SmtpServer.Send(mail);

           // //SMS 
           // String api = "https://lifetimesms.com/json?api_token=171eb8d799e611848327b8d598aa00d4656be45385&api_secret=PunjabiHut&to=" + o.ORDER_CONTACT + " &from=PunjabiHut&message=Your Order Has Been Booked....Thank You.";
           // HttpWebRequest req= (HttpWebRequest)WebRequest.Create(api);
           // var httpResponse = (HttpWebResponse)req.GetResponse();

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
                od.QUANTITY = p[i].PRO_QUANTITY*-1;
                od.PURCHASE_PRICE = p[i].PRODUCT_PURCHASEPRICE;
                od.SALE_PRICE = p[i].PRODUCT_SALEPRICE;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            if (Session["ExtraCart"] != null)
            {
                List<Extra> E = (List<Extra>)Session["ExtraCart"];
                for (int i = 0; i < E.Count; i++)
                {

                    OrderExtraDetail ad = new OrderExtraDetail();

                    int orderID = db.Orders.Max(x => x.ORDER_ID);
                    ad.PRODUCT_FID = E[i].Product.PRODUCT_ID;
                    ad.EXTRA_FID = E[i].EXTRA_ID;
                    ad.ORDER_FID = orderID;
                    db.OrderExtraDetails.Add(ad);
                    db.SaveChanges();
                }
            }
            return View();

        }

        public ActionResult CloseOrder()
        {
            Session["Order"] = null;
            Session["myCart"] = null;
            Session["ExtraCart"] = null;
            return RedirectToAction("IndexCustomer");
        }

    }

}