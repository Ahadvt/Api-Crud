using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.DTOS.ProductDTOS
{
    public class ProductListItemDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }

    }
}
