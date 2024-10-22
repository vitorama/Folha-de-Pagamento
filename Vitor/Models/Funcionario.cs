namespace Vitor.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public decimal ValorHora { get; set; }
        public int HorasTrabalhadas { get; set; }
    }
}
