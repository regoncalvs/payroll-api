using Stone.Payroll.Domain.Common.Enums;
using Stone.Payroll.Infrastructure.ReadModels;
using Stone.Payroll.Utils.Extensions;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Domain.Entities
{
    /// <summary>
    /// Representa um contracheque.
    /// </summary>
    public class PayStub
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PayStub"/>.
        /// </summary>
        public PayStub(EmployeeRead employee)
        {
            GrossSalary = employee.GrossSalary;
            Entries = employee.Entries ?? Enumerable.Empty<Entry>();
        }

        /// <summary>
        /// Obtém o mês de referência do contracheque.
        /// </summary>
        [JsonPropertyName("MesReferencia")]
        public string ReferenceMonth => DateTime.Today.ToString("MM/yyyy");

        /// <summary>
        /// Obtém o salário bruto do funcionário.
        /// </summary>
        [JsonPropertyName("SalarioBruto")]
        public decimal GrossSalary { get; private set; }

        /// <summary>
        /// Obtém os lançamentos do contracheque.
        /// </summary>
        [JsonPropertyName("Lancamentos")]
        public IEnumerable<Entry> Entries { get; private set; }

        /// <summary>
        /// Obtém o total de descontos do contracheque.
        /// </summary>
        [JsonPropertyName("TotalDescontos")]
        public decimal TotalDeductions => Entries
            .Where(x => x.Type == EntryTypeEnum.Deduction.GetDescriptionFromEnumValue())
            .Select(x => x.Value).Sum() * -1;

        /// <summary>
        /// Obtém o salário líquido do funcionário.
        /// </summary>
        [JsonPropertyName("SalarioLiquido")]
        public decimal NetSalary => GrossSalary + TotalDeductions;
    }
}
