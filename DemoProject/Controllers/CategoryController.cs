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
    public class CategoryController : Controller
    {
        Demo db = new Demo();
        // GET: Category
        public ActionResult Index()
        {
            List<Category> objlist = LoadContent();
            return View(objlist);
        }


        public List<Category> LoadContent()
        {
            List<Category> objlist = new List<Category>();
            try
            {
                var skip = 0;
                int PageNumber = 1;
                int Total = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(TempData["Current_Page"])))
                    PageNumber = Convert.ToInt32(TempData["Current_Page"]);
                else if (!string.IsNullOrEmpty(Convert.ToString(Request["Page"])))
                    PageNumber = Convert.ToInt32(Request["Page"]);



                objlist = CategoryBL.Instance.Getcategorydata(db);

               

                skip = 10 * (PageNumber - 1);
                Total = objlist.Count();
                ViewData["VDPager"] = new PaginationModel().CreatePager(PageNumber, Total);
                ViewData["Current_Page"] = PageNumber;
                return objlist.Skip(skip).Take(10).ToList();
            }
            catch(Exception ex)
            {
                return objlist;
            }
        }


        public ActionResult PartialIndex()
        {
            return PartialView(@"~\Views\Category\_ListCategory.cshtml", LoadContent());
        }

        public ActionResult CreateEdit(long id = 0)
        {
            Category model = new Category();
            if (id != 0)
            {
                ViewBag.Isupdate = true;
                model = CategoryBL.Instance.GetDataByPK(db, id);
            }
            else
            {
                ViewBag.Isupdate = false;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEdit(long id, Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getcategorydata = CategoryBL.Instance.Getcategorydata(db);
                    if (getcategorydata.Where(x => x.CategoryName.Contains(model.CategoryName)).Count() > 0)
                    {
                        ModelState.AddModelError("CategoryName", "Already Exist Category Name");
                        if (id != 0)
                        {
                            ViewBag.Isupdate = true;
                        }
                        else
                        {
                            ViewBag.Isupdate = false;
                        }
                        return View(model);
                    }
                    else
                    {
                        if (id != 0)
                        {
                            var getdata = CategoryBL.Instance.GetDataByPK(db, id);
                            getdata.CategoryName = model.CategoryName;
                            bool update = CategoryBL.Instance.UpdateCategory(db, getdata);
                            if (update)
                                TempData["category"] = "Category Updated Successfully";
                            else
                                TempData["category"] = "Category cannot updated";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            model.IsActive = true;
                            bool insert = CategoryBL.Instance.InsertCategory(db, model);
                            if (insert)
                                TempData["category"] = "Category inserted Successfully";
                            else
                                TempData["category"] = "Cannot insert category";
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
            var getdata = CategoryBL.Instance.GetDataByPK(db, id);
            if (getdata != null)
            {
                getdata.IsActive = false;
                bool delete = CategoryBL.Instance.UpdateCategory(db, getdata);

                if (delete)
                    TempData["category"] = "Category Deleted Successfully";
                else
                    TempData["category"] = "Cannot Delete Category";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["category"] = "No Data to Delete";
                return RedirectToAction("Index");
            }
        }


        public ActionResult Details(long id)
        {
            CategoryDisplayModel model = new CategoryDisplayModel();
            var getcategorydata = CategoryBL.Instance.GetDataByPK(db, id);
            model.productlist = ProductBL.Instance.GetProductDataByCategory(db, id);
            model.categoryname = getcategorydata.CategoryName;
            return View(model);
        }

    }
}