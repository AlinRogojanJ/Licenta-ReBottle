using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class RecyclingRecordDTO
    {
        public Guid RecyclingRecordId { get; set; }
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }
        public Guid OrderStatusId { get; set; }
        public int Amount { get; set; }
        public float MoneySaved { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; }
    }
}
