using System.ComponentModel;

namespace Stone.Payroll.Domain.Common.Enums
{
    /// <summary>
    /// Enumeração que representa o tipo de lançamento.
    /// </summary>
    public enum EntryTypeEnum
    {
        /// <summary>
        /// Desconto
        /// </summary>
        [Description("Desconto")]
        Deduction,

        /// <summary>
        /// Remuneração
        /// </summary>
        [Description("Remuneração")]
        Remuneration
    }
}
