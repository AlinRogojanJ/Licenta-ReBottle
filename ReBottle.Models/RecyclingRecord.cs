using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models
{
    public class RecyclingRecord
    {
        [Key][Required][NotNull]
        public Guid RecyclingRecordId { get; set; }

        [ForeignKey("UserId")][Required][NotNull]
        public Guid UserId { get; set; }

        [ForeignKey("LocationId")] [Required] [NotNull]
        public Guid LocationId { get; set; }

        [ForeignKey("OrderStatusId")] [Required] [NotNull]
        public Guid OrderStatusId { get; set; }

        public int Amount { get; set; }

        public float MoneySaved { get; set; }

        public string Method { get; set; }

        public DateTime Date { get; set; }

        public DateTime Created { get; set; }

        //1-to-many
        public Location Location { get; set; }

        //1-to-many
        public OrderStatus OrderStatus { get; set; }

        public User User { get; set; }
        

    }
}
