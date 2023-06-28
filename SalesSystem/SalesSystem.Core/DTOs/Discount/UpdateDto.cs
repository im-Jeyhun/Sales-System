using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.DTOs.Discount
{
    public class UpdateDto
    {
        public string Title { get; set; }
        public decimal Percentage { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
