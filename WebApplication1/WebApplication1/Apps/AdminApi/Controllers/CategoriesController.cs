using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Apps.DTOS;
using WebApplication1.Apps.DTOS.CategoryDTOS;
using WebApplication1.Data.Dal;
using WebApplication1.Data.Entities;
using WebApplication1.Extension;

namespace WebApplication1.Apps.AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create([FromForm]CategoryPostDto categoryPost)
        {
            Category category = new Category();

            if (categoryPost.ImageFile!=null)
            {
                category.Image = categoryPost.ImageFile.SaveImage(_env.WebRootPath, "Image/category");
            }
            category.Name = categoryPost.Name;
            _context.Categories.Add(category); 
            _context.SaveChanges();

            return StatusCode(201,category);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromForm] CategoryPostDto categoryPostDto)
        {
            Category Exsistcategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (Exsistcategory == null) return NotFound();
            if (categoryPostDto.ImageFile!=null)
            {
                Helpers.Helper.DeleteImg(_env.WebRootPath,"Image/category" ,Exsistcategory.Image);
                Exsistcategory.Image = categoryPostDto.ImageFile.SaveImage(_env.WebRootPath, "Image/category");
            }
            Exsistcategory.Name = categoryPostDto.Name;
            Exsistcategory.UpdateDate = DateTime.Now;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id )
        {
            Category category = _context.Categories.Include(c=>c.Products).Where(c=>!c.IsDeleted).FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            CategoryGetDto categoryGet = _mapper.Map<CategoryGetDto>(category);
            return StatusCode(200, categoryGet);
        }


        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Categories.Where(c => !c.IsDeleted);

            ListDto<CategoryListItemDto> listDto = new ListDto<CategoryListItemDto>
            {
                Items = query.Select(x => new CategoryListItemDto
                {
                    Name=x.Name,
                    Id=x.Id
                }).ToList(),
                TotalCount=query.Count()
                
            };
            return Ok(listDto);


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return BadRequest();
            category.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }

    }
}
