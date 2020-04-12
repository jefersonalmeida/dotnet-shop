using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "Campo deve conter o mínimo de 3 caracteres")]
        [MaxLength(60, ErrorMessage = "Campo deve conter o máximo de 60 caracteres")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Campo deve conter o máximo de 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo deve ser maior que zero")]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}