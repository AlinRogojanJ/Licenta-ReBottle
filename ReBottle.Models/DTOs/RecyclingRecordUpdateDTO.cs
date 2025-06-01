using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class RecyclingRecordUpdateDTO
    {
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }
        public Guid OrderStatusId { get; set; }
        public Guid ImageId { get; set; }
        public float MoneySaved { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; }
    }
}
