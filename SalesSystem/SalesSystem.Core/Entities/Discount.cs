using SalesSystem.Core.DataBase.Models.Common;
using SalesSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.Entities
{
    public class Discount : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public decimal Percentage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set ; }
        public DateTime ExpireDate { get ; set ; }
    }
}
