using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class OcrResult
    {
        public string receipt_id { get; set; }
        public string amount_ron { get; set; }
        public string unit_price { get; set; }
        public string quantity { get; set; }
        public string total_price { get; set; }
        public string barcode { get; set; }
        public string date { get; set; }
        public string raw_text { get; set; }
    }

}
