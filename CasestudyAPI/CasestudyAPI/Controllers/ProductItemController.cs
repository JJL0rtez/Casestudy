using System.Collections.Generic;
using CasestudyAPI.DAL;
using CasestudyAPI.DAL.DAO;
using CasestudyAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CasestudyAPI.Controllers
{
    [Authorize]
    [Route("api/product")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        AppDbContext _db;
        public ProductItemController(AppDbContext context)
        {
            _db = context;
        }
        [AllowAnonymous]
        [Route("{brandId}")]
        public ActionResult<List<Product>> Index(int brandId)
        {
            ProductItemDAO dao = new ProductItemDAO(_db);
            List<Product> itemsForBrand = dao.GetAllByBrand(brandId);
            return itemsForBrand;
        }
    }
}