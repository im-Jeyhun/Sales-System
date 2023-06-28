using SalesSystem.Core.DataBase.Models.Common;
using SalesSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.Entities
{
    public class Receipt : BaseEntity<int> , IAuditable
    {
        public string MarketAddress { get; set; }
        public int UserId { get; set; }
        public User User { get ; set; }
        public decimal Total { get; set; }
        public List<Product>? Products { get; set; }
        public Guid BarCode { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set ; }
    }
}
