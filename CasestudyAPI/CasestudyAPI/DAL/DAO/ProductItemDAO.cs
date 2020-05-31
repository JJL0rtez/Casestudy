using System.Collections.Generic;
using System.Linq;
using  CasestudyAPI.DAL.DomainClasses;

namespace CasestudyAPI.DAL.DAO
{
    public class ProductItemDAO
    {
        private AppDbContext _db;
        public ProductItemDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Product> GetAllByBrand(int id)
        {
            return _db.Products.Where(item => item.Brand.Id == id).ToList();
        }
    }
}