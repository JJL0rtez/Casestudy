using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CasestudyAPI.DAL;
using CasestudyAPI.DAL.DomainClasses;
namespace ServerAPI.DAL
{
    public class DataUtility
    {
        private AppDbContext _db;
        public DataUtility(AppDbContext context)
        {
            _db = context;
        }
    }
}