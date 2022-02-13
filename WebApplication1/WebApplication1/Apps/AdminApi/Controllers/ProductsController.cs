using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Apps.DTOS;
using WebApplication1.Apps.DTOS.ProductDTOS;
using WebApplication1.Data.Dal;
using WebApplication1.Data.Entities;

namespace WebApplication1.Apps.AdminApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context,IMapper mapper)
		{
			_context = context;
            _mapper = mapper;
        }

		[HttpPost("")]
		public IActionResult Create(ProductPostDtos productPost)
		{
			Product product = new Product
			{
				Name = productPost.Name,
				SalePrice = productPost.SalePrice,
				CostPrice = productPost.CostPrice,
				CategoryId = productPost.CategoryId
			};
			_context.Products.Add(product);
			_context.SaveChanges();
			return StatusCode(201, product);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		
		{
			Product product = _context.Products.FirstOrDefault(p => p.Id == id&&!p.IsDeleted);
			if (product == null) return NotFound();
			ProductGetDtos productGet = _mapper.Map<ProductGetDtos>(product);
			return Ok(productGet);
		}

		[HttpGet("")]
		public IActionResult GetAll(int page=1)
		{
			var query = _context.Products.Where(p => !p.IsDeleted);

			ListDto<ProductListItemDtos> listDto = new ListDto<ProductListItemDtos>
			{
				Items = query.Select(x => new ProductListItemDtos
				{
					Id = x.Id,
					Name = x.Name,
					SalePrice = x.SalePrice,
					CostPrice = x.CostPrice,
					CategoryId = x.CategoryId,
					IsDeleted = x.IsDeleted,
				}).ToList(),
				TotalCount = query.Count()

			};

			return Ok(listDto);
		}

		[HttpPut("{id}")]
		public IActionResult Edit(int id,ProductPostDtos productPost)
		{
			Product ExsistProduct = _context.Products.FirstOrDefault(p => p.Id == id);
			if (ExsistProduct == null) return NotFound();

			ExsistProduct.Name = productPost.Name;
			ExsistProduct.SalePrice = productPost.SalePrice;
			ExsistProduct.CostPrice = productPost.CostPrice;
			ExsistProduct.CategoryId = productPost.CategoryId;
			ExsistProduct.UpdateDate = DateTime.Now;
			_context.SaveChanges();
			return NoContent();
		   
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			Product product = _context.Products.FirstOrDefault(p => p.Id == id);
			if (product == null) return NotFound();
			product.IsDeleted = true;
			_context.SaveChanges();
			return NoContent();
		}
		

	   
	  

	}
}
