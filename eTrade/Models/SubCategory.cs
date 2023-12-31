﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using eTrade.Data.Base;

namespace eTrade.Models
{
    public class SubCategory : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Insert Slug")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Please Select Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
