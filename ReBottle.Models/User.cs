using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models
{
    public class User
    {
        [Key] [Required] [NotNull]
        public Guid UserId { get; set; }

        [ForeignKey("RecyclingRecordId")] [Required] [NotNull]
        public Guid RecyclingRecordId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public RecyclingRecord RecyclingRecord { get; set; }
    }
}
