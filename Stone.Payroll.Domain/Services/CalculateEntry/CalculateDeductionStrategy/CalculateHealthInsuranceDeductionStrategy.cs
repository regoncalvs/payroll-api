using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do Plano de Saúde.
    /// </summary>
    public class CalculateHealthInsuranceDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        /// <summary>
        /// Obtém a descrição do desconto do Plano de Saúde.
        /// </summary>
        public override string Description => "Plano de Saúde";

        /// <summary>
        /// Calcula o desconto do Plano de Saúde para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do Plano de Saúde.</returns>
        public override decimal Calculate(Employee employee)
        {
            return employee.HasHealthInsuranceDeduction ? 10 : 0;
        }
    }
}
