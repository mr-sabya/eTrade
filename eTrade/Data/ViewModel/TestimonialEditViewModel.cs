using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eTrade.Data.ViewModel
{
    public class TestimonialEditViewModel
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
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
