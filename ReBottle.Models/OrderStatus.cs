using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models
{
    public class OrderStatus
    {
        [Key] [Required] [NotNull]
        public Guid OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }

        //public ICollection<RecyclingRecord> RecyclingRecords { get; set; }
    }
}
