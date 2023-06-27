using eTrade.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTrade.Models
{
    public class Testimonial : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Company { get; set; }


        [Required]
        [DataType(DataType.Text)]
        public string Feedback { get; set; }


        [Display(Name = "Upload Name")]
        public string Image { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Please upload Client Image")]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
