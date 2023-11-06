using Stone.Payroll.Domain.Common.Enums;
using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;

namespace Stone.Payroll.Domain.Services.CalculateEntry
{
    /// <summary>
    /// Classe abstrata para cálculos de desconto.
    /// </summary>
    public abstract class CalculateDeduction : ICalculateEntry
    {
        /// <summary>
        /// Define o tipo de lançamento como desconto.
        /// </summary>
        EntryTypeEnum ICalculateEntry.EntryType => EntryTypeEnum.Deduction;

        /// <summary>
        /// Obtém a descrição do desconto.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Calcula o valor do desconto para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do desconto.</param>
        /// <returns>O valor do desconto calculado.</returns>
        public abstract decimal Calculate(Employee employee);
    }
}
