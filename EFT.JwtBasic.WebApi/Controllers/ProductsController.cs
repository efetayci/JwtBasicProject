using AutoMapper;
using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Entites.Concrete;
using EFT.JwtBasic.Entites.Dtos.ProductDtos;
using EFT.JwtBasic.WebApi.CustomFilters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFT.JwtBasic.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        [HttpGet]
        [ValidModel]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Product>))]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        [ValidModel]
        public async Task<IActionResult> Post(ProductAddDto productAddDto)
        {
            await productService.Add(this.mapper.Map<Product>(productAddDto));
            return Created("", productAddDto);
        }
        [HttpPut]
        [ValidModel]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await productService.Update(this.mapper.Map<Product>(productUpdateDto));
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidId<Product>))]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.Remove(new Product { Id = id });
            return NoContent();
        }

        [Route("/Error")]
        protected IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //errorInfo.Error.Meesage or stack tee with logger... 

            return Problem(detail: "Error occured in api will be fixed as soon as possible");
        }
    }
}
