using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class RecyclingReportDTO
    {
        public string Date { get; set; } = string.Empty;          
        public string Amount { get; set; } = string.Empty;         
        public string Progress { get; set; } = string.Empty;       
    }
}
