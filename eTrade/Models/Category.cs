﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using eTrade.Data.Base;

namespace eTrade.Models
{
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Insert Slug")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Please Insert Slug")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }


        //relationships
        public List<SubCategory> subCategories{ get; set; }
    }
}
