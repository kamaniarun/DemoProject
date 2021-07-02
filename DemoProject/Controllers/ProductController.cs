using BussinessLayer.BussinesLogic;
using DemoProject.Models;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class ProductController : Controller
    {
        Demo db = new Demo();
        // GET: Product
        public ActionResult Index()
        {
            List<ProductDisplayModel> objmodel = new List<ProductDisplayModel>();
            objmodel = LoadContent();
            return View(objmodel);
        }

        public List<ProductDisplayModel> LoadContent()
        {
            List<ProductDisplayModel> objmodel = new List<ProductDisplayModel>();
            try
            {
                var skip = 0;
                int PageNumber = 1;
                int Total = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(TempData["Current_Page"])))
                    PageNumber = Convert.ToInt32(TempData["Current_Page"]);
                else if (!string.IsNullOrEmpty(Convert.ToString(Request["Page"])))
                    PageNumber = Convert.ToInt32(Request["Page"]);

                var getproductdata = ProductBL.Instance.GetProductData(db);
                Total = getproductdata.Count();
                skip = 10 * (PageNumber - 1);
                getproductdata= getproductdata.Skip(skip).Take(10).ToList();

                foreach (var x in getproductdata)
                {
                    ProductDisplayModel model = new ProductDisplayModel();
                    model.Id = x.Id;
                    var getcategoryname = CategoryBL.Instance.GetDataByPK(db, (long)x.CategoryId);
                    if (getcategoryname != null)
                        model.CategoryName = getcategoryname.CategoryName;
                    else
                        model.CategoryName = "";
                    model.ProductName = x.ProductName;
                    objmodel.Add(model);
                }

               
                ViewData["VDPager"] = new PaginationModel().CreatePager(PageNumber, Total);
                ViewData["Current_Page"] = PageNumber;
                return objmodel;
            }
            catch(Exception ex)
            {
                return objmodel;
            }
        }

        public ActionResult PartialIndex()
        {
            return PartialView(@"~\Views\Product\_ListProduct.cshtml", LoadContent());
        }

        public ActionResult CreateEdit(long id = 0)
        {
            List<SelectListItem> lstResult = new List<SelectListItem>();
            lstResult.Add(new SelectListItem { Text = "--Select--", Value = "" });
            ProductDisplayModel model = new ProductDisplayModel();
            if (id != 0)
            {
                ViewBag.Isupdate = true;
                var getdata = ProductBL.Instance.GetDataByPK(db, id);
                model.ProductName = getdata.ProductName;
                model.Categoryid = (long)getdata.CategoryId;
            }
            else
            {
                ViewBag.Isupdate = false;
            }


            var getcategorydata = CategoryBL.Instance.Getcategorydata(db).ToList();
            foreach(Category item in getcategorydata)
            {
                lstResult.Add(new SelectListItem { Text = item.CategoryName, Value = Convert.ToString(item.Id) });
            }

            ViewBag.Category = lstResult;
            return View(model);
        }



        [HttpPost]
        public ActionResult CreateEdit(long id, ProductDisplayModel model)
        {
            try
            {
                List<SelectListItem> lstResult = new List<SelectListItem>();
                lstResult.Add(new SelectListItem { Text = "--Select--", Value = "" });

                if (ModelState.IsValid)
                {
                    var getproductdata = ProductBL.Instance.GetProductData(db);
                    if (getproductdata.Where(x => x.ProductName.Contains(model.ProductName) && x.Id!=model.Id).Count() > 0)
                    {
                        ModelState.AddModelError("ProductName", "Already Exist Product Name");
                        if (id != 0)
                        {
                            ViewBag.Isupdate = true;
                           
                        }
                        else
                        {
                            ViewBag.Isupdate = false;
                        }

                        var getcategorydata = CategoryBL.Instance.Getcategorydata(db).ToList();
                        foreach (Category item in getcategorydata)
                        {
                            lstResult.Add(new SelectListItem { Text = item.CategoryName, Value = Convert.ToString(item.Id) });
                        }

                        ViewBag.Category = lstResult;

                        return View(model);
                    }
                    else
                    {
                        if (id != 0)
                        {
                            var getdata = ProductBL.Instance.GetDataByPK(db, id);
                            getdata.ProductName = model.ProductName;
                            getdata.CategoryId = model.Categoryid;
                            bool update = ProductBL.Instance.UpdateProduct(db, getdata);
                            if (update)
                                TempData["product"] = "Product Updated Successfully";
                            else
                                TempData["product"] = "Product cannot updated";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Product Model = new Product();
                            Model.ProductName = model.ProductName;
                            Model.CategoryId = model.Categoryid;
                            Model.IsActive = true;
                            bool insert = ProductBL.Instance.InsertProduct(db, Model);
                            if (insert)
                                TempData["product"] = "Product inserted Successfully";
                            else
                                TempData["product"] = "Cannot insert Product";
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    if (id != 0)
                    {
                        ViewBag.Isupdate = true;
                    }
                    else
                    {
                        ViewBag.Isupdate = false;
                    }

                    var getcategorydata = CategoryBL.Instance.Getcategorydata(db).ToList();
                    foreach (Category item in getcategorydata)
                    {
                        lstResult.Add(new SelectListItem { Text = item.CategoryName, Value = Convert.ToString(item.Id) });
                    }

                    ViewBag.Category = lstResult;



                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }

        }


        public ActionResult Delete(long id)
        {
            var getdata = ProductBL.Instance.GetDataByPK(db, id);
            if (getdata != null)
            {
                getdata.IsActive = false;
                bool delete = ProductBL.Instance.UpdateProduct(db, getdata);

                if (delete)
                    TempData["product"] = "Product Deleted Successfully";
                else
                    TempData["product"] = "Product Delete Category";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["product"] = "No Data to Delete";
                return RedirectToAction("Index");
            }
        }


    }
}