using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "Campo deve conter o mínimo de 3 caracteres")]
        [MaxLength(60, ErrorMessage = "Campo deve conter o máximo de 60 caracteres")]
        public string Title { get; set; }
    }
}