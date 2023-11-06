using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do FGTS.
    /// </summary>
    public class CalculateFgtsDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        private readonly decimal TaxRate = 0.08m;

        /// <summary>
        /// Obtém a descrição do desconto do FGTS.
        /// </summary>
        public override string Description => "FGTS";

        /// <summary>
        /// Calcula o desconto do FGTS para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do FGTS.</returns>
        public override decimal Calculate(Employee employee)
        {
            return employee.GrossSalary * TaxRate;
        }
    }
}
