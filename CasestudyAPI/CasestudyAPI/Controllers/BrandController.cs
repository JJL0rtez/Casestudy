using System;
using System.Collections.Generic;
using CasestudyAPI.DAL;
using CasestudyAPI.DAL.DAO;
using CasestudyAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CasestudyAPI.Controllers
{
    [Authorize]
    [Route("api/brand")]
    [ApiController]
    public class BandController : ControllerBase
    {
        AppDbContext _db;
        public BandController(AppDbContext context)
        {
            _db = context;
        }
        [AllowAnonymous]
        public ActionResult<List<Brand>> Index()
        {
            BrandDAO dao = new BrandDAO(_db);
            List<Brand> allBrands = dao.GetAll();
            Console.WriteLine("in here");
            Console.WriteLine(allBrands.Count);
            return allBrands;
        }
    }
}