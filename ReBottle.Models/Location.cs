using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models
{
    public class Location
    {
        [Key] [Required] [NotNull]
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public byte[] Image { get; set; }
        public string Schedule { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<RecyclingRecord> RecyclingRecords { get; set; }

    }
}
