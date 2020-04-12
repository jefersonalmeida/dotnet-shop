using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "Campo deve conter o mínimo de 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Campo deve conter o máximo de 100 caracteres")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "Campo deve conter o mínimo de 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Campo deve conter o máximo de 100 caracteres")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}