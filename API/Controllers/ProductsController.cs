using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        #region Fields
        private readonly IGenericRepositotry<Product> _productsRepo;
        private readonly IGenericRepositotry<ProductBrand> _productBrandRepo;
        private readonly IGenericRepositotry<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public ProductsController(IGenericRepositotry<Product> productsRepo, IGenericRepositotry<ProductBrand> productBrandRepo,
            IGenericRepositotry<ProductType> productTypeRepo, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItem = await _productsRepo.CountAsync(countSpec);

            var Products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize,totalItem,data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }


        #endregion






    }
}
