using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTrade.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Display(Name = "Upload Name")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
