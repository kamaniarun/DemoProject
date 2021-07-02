using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataLayer.DataLogic
{
    public class CategoryDL : Category
    {
        private static readonly Lazy<CategoryDL> _instance = new Lazy<CategoryDL>(() => new CategoryDL());

        public static CategoryDL Instance
        {
            get { return _instance.Value; }
        }
        private CategoryDL() { }

        public Category GetDataByPK(Demo db, long Id)
        {
            var data = db.Categories.Where(x => x.Id == Id).FirstOrDefault();
            return data;
        }

        public List<Category> Getcategorydata(Demo db)
        {
            var data = db.Categories.Where(x => x.IsActive==true).ToList();
            return data;
        }

        public bool UpdateCategory(Demo db,Category Model)
        {
            try
            {
                var update = db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool InsertCategory(Demo db, Category Model)
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