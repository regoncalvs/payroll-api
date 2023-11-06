using Stone.Payroll.Domain.Common.Enums;
using Stone.Payroll.Domain.Entities;

namespace Stone.Payroll.Domain.Services.CalculateDeductionStrategy
{
    /// <summary>
    /// Classe para cálculo de remuneração.
    /// </summary>
    public class CalculateRemuneration : ICalculateEntry
    {
        /// <summary>
        /// Define o tipo de lançamento como remuneração.
        /// </summary>
        EntryTypeEnum ICalculateEntry.EntryType => EntryTypeEnum.Remuneration;

        /// <summary>
        /// Obtém a descrição da remuneração.
        /// </summary>
        public string Description => "Salário";

        /// <summary>
        /// Calcula o valor da remuneração para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo da remuneração.</param>
        /// <returns>O valor da remuneração calculado.</returns>
        public decimal Calculate(Employee employee)
        {
            return employee.GrossSalary;
        }
    }
}
