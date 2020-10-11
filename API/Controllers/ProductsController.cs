using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Entities;
using Core.Interfaces;
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
        private readonly IProductRepository _repo;
        #endregion

        #region ctor
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var Products = await _repo.GetProductsAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Products = await _repo.GetProductByIdAsync(id);
            return Ok(Products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }


        #endregion






    }
}
