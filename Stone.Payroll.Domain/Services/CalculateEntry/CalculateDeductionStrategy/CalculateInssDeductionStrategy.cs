using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do INSS.
    /// </summary>
    public class CalculateInssDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        /// <summary>
        /// Obtém a descrição do desconto do INSS.
        /// </summary>
        public override string Description => "INSS";

        private readonly decimal AliquotaMaxima = 0.14m;

        private readonly Dictionary<decimal, decimal> DeductionTiers = new()
        {
            { 1045m, 0.075m },
            { 2089.6m, 0.09m },
            { 3134.4m, 0.12m },
            { 6101.06m, 0.14m }
        };

        /// <summary>
        /// Calcula o desconto do INSS para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do INSS.</returns>
        public override decimal Calculate(Employee employee)
        {
            var grossSalary = employee.GrossSalary;
            foreach (var tier in DeductionTiers.Keys)
            {
                if (grossSalary <= tier)
                {
                    return grossSalary * DeductionTiers[tier];
                }
            }

            return grossSalary * AliquotaMaxima;
        }
    }
}
