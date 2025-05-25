using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace ReBottle.Models
{
    public class ImageStorage
    {
        [Key] [Required] [NotNull]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("UserId")] [Required] [NotNull]
        public Guid UserId { get; set; }
        public string FileName { get; set; } 
        public string ContentType { get; set; } 
        public byte[] Data { get; set; } 
        public User User { get; set; } 
    }
}
