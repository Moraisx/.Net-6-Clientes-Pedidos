using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiClientesPedidos.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        [Required]
        [StringLength(255)]
        [MaxLength(255)]
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        [Required]
        [StringLength(45)]
        [MaxLength(45)]
        public string? Cep { get; set; }
        [Required]
        [StringLength(255)]
        [MaxLength(255)]
        public string? Bairro { get; set; }
        [Required]
        [StringLength(255)]
        [MaxLength(255)]
        public string? Cidade { get; set; }
      
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }
    }
}
