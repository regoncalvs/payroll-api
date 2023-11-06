using MediatR;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Application.Commands.CreateEmployee
{
    /// <summary>
    /// Comando para criar um novo funcionário.
    /// </summary>
    public class CreateEmployeeCommand : IRequest<CreateEmployeeResponse>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateEmployeeCommand"/>.
        /// </summary>
        public CreateEmployeeCommand(
            string name,
            string surname,
            string document,
            string department,
            decimal? grossSalary,
            DateTime? admissionDate,
            bool? hasHealthInsuranceDeduction,
            bool? hasDentalPlanDeduction,
            bool? hasTransportationVoucherDeduction)
        {
            Name = name;
            Surname = surname;
            Document = document;
            Department = department;
            GrossSalary = grossSalary;
            AdmissionDate = admissionDate;
            HasHealthInsuranceDeduction = hasHealthInsuranceDeduction;
            HasDentalPlanDeduction = hasDentalPlanDeduction;
            HasTransportationVoucherDeduction = hasTransportationVoucherDeduction;
        }

        /// <summary>
        /// Obtém ou define o nome do funcionário.
        /// </summary>
        [JsonPropertyName("Nome")]
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define o sobrenome do funcionário.
        /// </summary>
        [JsonPropertyName("Sobrenome")]
        public string Surname { get; set; }

        /// <summary>
        /// Obtém ou define o documento do funcionário.
        /// </summary>
        [JsonPropertyName("Documento")]
        public string Document { get; set; }

        /// <summary>
        /// Obtém ou define o setor do funcionário.
        /// </summary>
        [JsonPropertyName("Setor")]
        public string Department { get; set; }

        /// <summary>
        /// Obtém ou define o salário bruto do funcionário.
        /// </summary>
        [JsonPropertyName("SalarioBruto")]
        public decimal? GrossSalary { get; set; }

        /// <summary>
        /// Obtém ou define a data de admissão do funcionário.
        /// </summary>
        [JsonPropertyName("DataAdmissao")]
        public DateTime? AdmissionDate { get; set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano de saúde.
        /// </summary>
        [JsonPropertyName("PossuiDescontoPlanoSaude")]
        public bool? HasHealthInsuranceDeduction { get; set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano dental.
        /// </summary>
        [JsonPropertyName("PossuiDescontoPlanoDental")]
        public bool? HasDentalPlanDeduction { get; set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no vale transporte.
        /// </summary>
        [JsonPropertyName("PossuiDescontoValeTransporte")]
        public bool? HasTransportationVoucherDeduction { get; set; }
    }
}
