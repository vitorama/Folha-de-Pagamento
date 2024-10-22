namespace Vitor.Models
{
    public class Folha
    {
        public int FolhaId { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal ImpostoDeRenda { get; set; }
        public decimal INSS { get; set; }
        public decimal FGTS { get; set; }
        public decimal SalarioLiquido { get; set; }

        public void CalcularFolha(Funcionario funcionario)
        {
            SalarioBruto = funcionario.HorasTrabalhadas * funcionario.ValorHora;
            ImpostoDeRenda = CalcularImpostoDeRenda(SalarioBruto);
            INSS = CalcularINSS(SalarioBruto);
            FGTS = SalarioBruto * 0.08m;
            SalarioLiquido = SalarioBruto - ImpostoDeRenda - INSS;
        }

        private decimal CalcularImpostoDeRenda(decimal salarioBruto)
        {
            if (salarioBruto <= 1903.98m)
                return 0;
            if (salarioBruto <= 2826.65m)
                return salarioBruto * 0.075m - 142.80m;
            if (salarioBruto <= 3751.05m)
                return salarioBruto * 0.15m - 354.80m;
            if (salarioBruto <= 4664.68m)
                return salarioBruto * 0.225m - 636.13m;
            return salarioBruto * 0.275m - 869.36m;
        }

        private decimal CalcularINSS(decimal salarioBruto)
        {
            if (salarioBruto <= 1693.72m)
                return salarioBruto * 0.08m;
            if (salarioBruto <= 2822.90m)
                return salarioBruto * 0.09m;
            if (salarioBruto <= 5645.80m)
                return salarioBruto * 0.11m;
            return 621.03m; // Fixo acima de 5645.80
        }
    }
}
