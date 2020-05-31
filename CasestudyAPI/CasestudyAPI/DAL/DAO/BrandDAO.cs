using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasestudyAPI.DAL.DAO;

namespace CasestudyAPI.DAL.DAO
{
    public class BrandDAO
    {
        private AppDbContext _db;
        public BrandDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Brand> GetAll()
        {
            List<Brand> test = new List<Brand>();
           test =  _db.Brands.ToList<Brand>();

            return test;
        }
    }
}


