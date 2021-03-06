﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordServices
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordServices(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var salesrecord = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                salesrecord = salesrecord.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                salesrecord = salesrecord.Where(x => x.Date <= maxDate.Value);
            }
            return await salesrecord.Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
  
    }
}
