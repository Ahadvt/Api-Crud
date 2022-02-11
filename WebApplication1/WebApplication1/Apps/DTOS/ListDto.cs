using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Apps.DTOS
{
    public class ListDto<TItems>
    {
        public int TotalCount { get; set; }
        public List<TItems> Items { get; set; }
    }
}
