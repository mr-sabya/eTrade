using eTrade.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTrade.Models
{
    public class Banner : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Link { get; set; }

        [Display(Name = "Upload Name")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
