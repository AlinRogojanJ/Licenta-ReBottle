using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class LocationDTO
    {
        public string LocationName { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public byte[] Image { get; set; }
        public string Schedule { get; set; }
    }
}
