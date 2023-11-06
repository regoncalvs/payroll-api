using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do IRRF.
    /// </summary>
    public class CalculateIrrfDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        /// <summary>
        /// Obtém a descrição do desconto do IRRF.
        /// </summary>
        public override string Description => "IRRF";

        private readonly decimal MaxTaxRate = 0.275m;
        private readonly decimal MaxCeiling = 869.36m;

        private readonly Dictionary<decimal, (decimal Aliquota, decimal Teto)> DeductionTiersIrrf = new()
        {
            { 1903.98m, (0m, 0m) },
            { 2826.65m, (0.075m, 142.8m) },
            { 3751.05m, (0.15m, 354.8m) },
            { 4664.68m, (0.225m, 636.13m) }
        };

        /// <summary>
        /// Calcula o desconto do IRRF para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do IRRF.</returns>
        public override decimal Calculate(Employee employee)
        {
            var grossSalary = employee.GrossSalary;
            foreach (var tier in DeductionTiersIrrf.Keys)
            {
                if (grossSalary <= tier)
                {
                    var (taxRate, ceiling) = DeductionTiersIrrf[tier];
                    var deductionIrrf = grossSalary * taxRate;
                    return Math.Min(deductionIrrf, ceiling);
                }
            }

            return Math.Min(grossSalary * MaxTaxRate, MaxCeiling);
        }
    }
}
