using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RH_Management.API.Models
{
    public class Ferias
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Data de término é obrigatória")]
        public DateTime DataTermino { get; set; }

        public string? Status { get; set; } 

        [Required(ErrorMessage = "ID do funcionário é obrigatório")]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        [JsonIgnore]  
        public virtual Funcionario? Funcionario { get; set; } 
    }
}