using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "A Product Id is required.") ]
        [MinLength(5)]
        [StringLength(5, ErrorMessage = "Product ID must be 5 Characters long.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "A Product Description is required.")]
        [MaxLength(100, ErrorMessage = "Description must be less then 100 Characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A Product Model is required.")]
        [MaxLength(20, ErrorMessage = "Model must be less then 20 Characters.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "A Product Brand is required.")]
        [MaxLength(20, ErrorMessage = "Brand must be less then 20 Characters.")]
        public string Brand { get; set; }
    }
}
