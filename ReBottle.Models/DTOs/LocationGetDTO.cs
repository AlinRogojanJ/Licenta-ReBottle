using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class LocationGetDTO
    {
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public byte[] Image { get; set; }
        public string Schedule { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
