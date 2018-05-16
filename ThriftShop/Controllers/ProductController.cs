using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriftShop.Models;

namespace ThriftShop.Controllers
{
    public class ProductController : Controller
    {
        private ShopStoreContext context;

        void setDbContext()
        {
            if (context == null)
                context = HttpContext.RequestServices.GetService(typeof(ShopStoreContext)) as ShopStoreContext;
        }

        // GET: Product
        public ActionResult Index()
        {
            setDbContext();

            List<FullProduct> list = new List<FullProduct>();
            List<Product> products = new List<Product>();
            List<Brand> brands = new List<Brand>();

            products = context.GetAllProducts();

            brands = context.GetAllBrands();

            List<Product> fullProducts = (from product in products
                                          join brand in brands
                                          on product.BrandId equals brand.Id
                                          select new Product
                                          {
                                              brand = brand,
                                              Name = product.Name,
                                              Price = product.Price
                                          ,
                                              Category = product.Category,
                                              Id = product.Id,
                                              BrandId = product.BrandId
                                          }).ToList<Product>();

            return View(fullProducts);
        }


        // GET: Product/Filter
        public ActionResult Filter(decimal price)
        {
            setDbContext();

            List<FullProduct> list = new List<FullProduct>();
            List<Product> products = new List<Product>();
            List<Brand> brands = new List<Brand>();

            products = context.GetAllProducts();

            brands = context.GetAllBrands();


            List<Product> fullProducts = (from product in products
                                          join brand in brands
                                          on product.BrandId equals brand.Id
                                          where product.Price <= price
                                          select new Product
                                          {
                                              brand = brand,
                                              Name = product.Name,
                                              Price = product.Price
                                          ,
                                              Category = product.Category,
                                              Id = product.Id,
                                              BrandId = product.BrandId
                                          }).ToList<Product>();

            return View(fullProducts);
        }


        // GET: Product/ListOfBrands
        public ActionResult ListOfBrands()
        {
            setDbContext();

            List<FullProduct> list = new List<FullProduct>();
            List<Product> products = new List<Product>();
            List<Brand> brands = new List<Brand>();

            products = context.GetAllProducts();

            brands = context.GetAllBrands();

            List<BrandResult> ListOfBrands = (from brand in brands
                                join product in products
                                on brand.Id equals product.BrandId
                                into g
                                select new BrandResult
                                {
                                    BrandName = brand.BrandName,
                                    NumOfProducts = g.Count()
                                }).ToList<BrandResult>();

            return View(ListOfBrands); 
        }

        // GET: Product/SortedProducts
        public ActionResult SortedProducts(String sortBy,String order)
        {
            setDbContext();

            List<FullProduct> list = new List<FullProduct>();
            List<Product> products = new List<Product>();

            products = context.GetAllProducts();

            List<Product> FilteredList = new List<Product>();

            if (sortBy != null)
            {
                if (sortBy.Equals("Name"))
                {
                    if (order != null)
                    {
                        if (order.Equals("Ascending"))
                        {
                            FilteredList = (from product in products
                                            orderby product.Name
                                            ascending
                                            select product).ToList<Product>();

                        }
                        else if (order.Equals("Descending"))
                        {
                            FilteredList = (from product in products
                                            orderby product.Name
                                            descending
                                            select product).ToList<Product>();

                        }
                    }
                }
                else if (sortBy.Equals("Price"))
                {
                    if (order != null)
                    {
                        if (order.Equals("Ascending"))
                        {
                            FilteredList = (from product in products
                                            orderby product.Price
                                            ascending
                                            select product).ToList<Product>();

                        }
                        else if (order.Equals("Descending"))
                        {
                            FilteredList = (from product in products
                                            orderby product.Price
                                            descending
                                            select product).ToList<Product>();

                        }
                    }
                }
                
            }



            return View(FilteredList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]Product product)
        {
            try
            {
                // TODO: Add insert logic here
                setDbContext();

                int status = context.AddProductIntoDB(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}