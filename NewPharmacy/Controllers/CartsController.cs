using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewPharmacy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewPharmacy.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public CartsController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); //for accessing current user
        }

        // GET: Carts
        public ActionResult Index()
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            ViewBag.CurrentUser = currentUser;

            var pendingItems = db.Carts.Include(pr => pr.Product)
                .Where(u => u.User.Id == currentUser.Id)
                .Where(p => p.IsCheckedOut == false).ToList();

            var pendingOrders = db.Orders.Where(c => c.Customer.Id == currentUser.Id)
                .Where(d => d.Delivered == false).ToList();

            decimal itemsSubTotal = 0;

            if(pendingItems != null)
            {
                foreach (var item in pendingItems)
                {
                    itemsSubTotal += item.Price * item.OrderQuantity;
                }
            }

            NewPharmacy.Models.ViewModels.CartOrderViewModel viewModel = new Models.ViewModels.CartOrderViewModel
            {
                PendingItems = pendingItems,
                PendingOrders = pendingOrders,
                ItemsSubTotal = itemsSubTotal
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            Cart cart = new Cart();
            cart.User = UserManager.FindById(User.Identity.GetUserId());

            var product = db.Products.Find(productId);
            cart.Product = product;
            cart.Price = product.Price;
            cart.OrderQuantity = quantity;
            cart.Total = product.Price * quantity;
            cart.IsCheckedOut = false;
            db.Carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        public ActionResult Checkout()
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            ViewBag.CurrentUser = currentUser;

            var pendingItems = db.Carts.Include(pr => pr.Product)
                .Where(u => u.User.Id == currentUser.Id)
                .Where(p => p.IsCheckedOut == false).ToList();

            decimal itemsSubTotal = 0;

            if (pendingItems != null)
            {
                foreach (var item in pendingItems)
                {
                    itemsSubTotal += item.Price * item.OrderQuantity;
                    item.IsCheckedOut = true;
                }
                db.SaveChanges();

                Order order = new Order
                {
                    Customer = currentUser,
                    CartSet = pendingItems,
                    SubTotal = itemsSubTotal,
                    Delivered = false,
                    OrderedDate = DateTime.UtcNow
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
