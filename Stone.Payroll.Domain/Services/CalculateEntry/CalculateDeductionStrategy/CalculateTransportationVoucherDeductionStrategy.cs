using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    /// <summary>
    /// Estratégia para calcular o desconto do Vale Transporte.
    /// </summary>
    public class CalculateTransportationVoucherDeductionStrategy : CalculateDeduction, ICalculateEntry
    {
        /// <summary>
        /// Obtém a descrição do desconto do Vale Transporte.
        /// </summary>
        public override string Description => "Vale Transporte";

        /// <summary>
        /// Calcula o desconto do Vale Transporte para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto do Vale Transporte.</returns>
        public override decimal Calculate(Employee employee)
        {
            if (employee.HasTransportationVoucherDeduction && employee.GrossSalary >= 1500)
                return employee.GrossSalary * 0.06m;

            return 0m;
        }
    }
}
