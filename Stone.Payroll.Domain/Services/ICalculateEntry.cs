using Stone.Payroll.Domain.Common.Enums;
using Stone.Payroll.Domain.Entities;

namespace Stone.Payroll.Domain.Services.CalculateDeductionStrategy
{
    /// <summary>
    /// Interface para cálculos de lançamento.
    /// </summary>
    public interface ICalculateEntry
    {
        /// <summary>
        /// Calcula o valor do lançamento para um funcionário.
        /// </summary>
        /// <param name="employee">O funcionário para o cálculo do lançamento.</param>
        /// <returns>O valor do lançamento calculado.</returns>
        decimal Calculate(Employee employee);

        /// <summary>
        /// Obtém a descrição do lançamento.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Obtém o tipo de lançamento.
        /// </summary>
        EntryTypeEnum EntryType { get; }
    }
}
