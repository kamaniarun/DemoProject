using DataLayer.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer;

namespace BussinessLayer.BussinesLogic
{
    public class ProductBL
    {
        private static readonly Lazy<ProductBL> _instance = new Lazy<ProductBL>(() => new ProductBL());

        public static ProductBL Instance
        {
            get { return _instance.Value; }
        }
        private ProductBL() { }

        public Product GetDataByPK(Demo db, long Id)
        {
            var data = ProductDL.Instance.GetDataByPK(db,Id);
            return data;
        }

        public List<Product> GetProductData(Demo db)
        {
            var data = ProductDL.Instance.GetProductData(db);
            return data;
        }
        public List<Product> GetProductDataByCategory(Demo db,long categoryid)
        {
            var data = ProductDL.Instance.GetProductDataByCategory(db, categoryid);
            return data;
        }
        public bool UpdateProduct(Demo db, Product model)
        {
            var update = ProductDL.Instance.UpdateProduct(db, model);
            return update;
        }

        public bool InsertProduct(Demo db, Product model)
        {
            var insert = ProductDL.Instance.InsertProduct(db, model);
            return insert;
        }

    }
}