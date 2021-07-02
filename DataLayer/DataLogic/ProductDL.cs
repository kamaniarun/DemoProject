using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataLayer.DataLogic
{
    public class ProductDL:Product
    {
        private static readonly Lazy<ProductDL> _instance = new Lazy<ProductDL>(() => new ProductDL());

        public static ProductDL Instance
        {
            get { return _instance.Value; }
        }
        private ProductDL() { }

        public Product GetDataByPK(Demo db, long Id)
        {
            var data = db.Products.Where(x => x.Id == Id).FirstOrDefault();
            return data;
        }

        public List<Product> GetProductData(Demo db)
        {
            var data = db.Products.Where(x => x.IsActive == true).ToList();
            return data;
        }
        public List<Product> GetProductDataByCategory(Demo db,long categoryid)
        {
            var data = db.Products.Where(x => x.IsActive == true && x.CategoryId== categoryid).ToList();
            return data;
        }

        public bool UpdateProduct(Demo db, Product Model)
        {
            try
            {
                var update = db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertProduct(Demo db, Product Model)
        {
            try
            {
                var insert = db.Entry(Model).State = EntityState.Added;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}