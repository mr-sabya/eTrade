using System.ComponentModel.DataAnnotations;

namespace eTrade.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}
