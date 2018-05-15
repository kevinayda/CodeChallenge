using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Entities
{
    public class Product
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Description { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max Length is 20")]
        public string Model { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max Length is 20")]
        public string Brand { get; set; }
    }
}
