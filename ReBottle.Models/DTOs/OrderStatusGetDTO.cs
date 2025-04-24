using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class OrderStatusGetDTO
    {
        public Guid OrderStatusId { get; set; }
        public string OrderStatusName { get; set; }
    }
}
