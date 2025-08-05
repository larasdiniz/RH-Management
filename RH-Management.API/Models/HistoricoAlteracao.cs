using System.ComponentModel.DataAnnotations.Schema;

namespace RH_Management.API.Models
{
    public class HistoricoAlteracao
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
        public string CampoAlterado { get; set; }
        public string? ValorAntigo { get; set; } 
        public string? ValorNovo { get; set; }   

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }
    }
}