using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do plano dental.
    /// </summary>
    public class CalculateDentalPlanDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        /// <summary>
        /// Obtém a descrição do desconto do plano dental.
        /// </summary>
        public override string Description => "Plano Dental";

        /// <summary>
        /// Calcula o desconto do plano dental para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do plano dental.</returns>
        public override decimal Calculate(Employee employee)
        {
            return employee.HasDentalPlanDeduction ? 5 : 0;
        }
    }
}
