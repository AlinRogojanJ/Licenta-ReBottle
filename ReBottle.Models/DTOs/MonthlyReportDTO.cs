using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class MonthlyReportDTO
    {
        public string Month { get; set; } = string.Empty;
        public float TotalBottles { get; set; }
    }

}
