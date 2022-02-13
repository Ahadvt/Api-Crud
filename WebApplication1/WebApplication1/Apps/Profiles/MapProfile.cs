using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Apps.DTOS.CategoryDTOS;
using WebApplication1.Apps.DTOS.ProductDTOS;
using WebApplication1.Data.Entities;

namespace WebApplication1.Apps.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductGetDtos>();
            CreateMap<Category, CategoryInProductGetDtos>();
            CreateMap<Category, CategoryGetDto>();
        }
    }
}
