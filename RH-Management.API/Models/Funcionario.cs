namespace RH_Management.API.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Ferias>? Ferias { get; set; }
        public virtual ICollection<HistoricoAlteracao>? HistoricoAlteracoes { get; set; }
    }
}