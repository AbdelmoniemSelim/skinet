using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Fields
        private readonly StoreContext _context;
        #endregion

        #region ctor
        public ProductsController(StoreContext context)
        {
            _context = context;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var Products = await _context.Products.ToListAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Products = await _context.Products.FindAsync(id);
            return Ok(Products);
        }
        #endregion






    }
}
