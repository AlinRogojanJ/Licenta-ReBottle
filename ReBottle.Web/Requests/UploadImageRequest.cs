using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ReBottle.Web.Requests
{
    public class UploadImageRequest
    {
        [Required]
        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = null!;

        [Required]
        [FromForm(Name = "userId")]
        public Guid UserId { get; set; }

        [FromForm(Name = "recyclingRecordId")]
        public Guid RecyclingRecordId { get; set; }
    }
}
