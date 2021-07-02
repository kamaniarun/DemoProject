using DataLayer.DataLogic;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BussinessLayer.BussinesLogic
{
    public class CategoryBL
    {

        private static readonly Lazy<CategoryBL> _instance = new Lazy<CategoryBL>(() => new CategoryBL());

        public static CategoryBL Instance
        {
            get { return _instance.Value; }
        }
        private CategoryBL() { }


        public Category GetDataByPK(Demo db, long Id)
        {
            var data = CategoryDL.Instance.GetDataByPK(db, Id);
            return data;
        }

        public List<Category> Getcategorydata(Demo db)
        {
            var data = CategoryDL.Instance.Getcategorydata(db);
            return data;
        }

        public bool UpdateCategory(Demo db,Category model)
        {
            var update= CategoryDL.Instance.UpdateCategory(db,model);
            return update;
        }

        public bool InsertCategory(Demo db,Category model)
        {
            var insert = CategoryDL.Instance.InsertCategory(db, model);
            return insert;
        }
    }
}