using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace ApiClientesPedidos.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(80)]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres")]
        public string? PrimeiroNome { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório")]
        [MaxLength(80)]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres")]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório")]
        [MaxLength(45)]
        [StringLength(45)]
        public string? Telefone { get; set; }
    }
}
