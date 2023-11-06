using System.Text.Json.Serialization;

namespace Stone.Payroll.Application.Queries.GetEmployee
{
    /// <summary>
    /// Representa a resposta da consulta de um funcionário.
    /// </summary>
    public record GetEmployeeResponse(
        [property: JsonPropertyName("Nome")] string Name,
        [property: JsonPropertyName("Sobrenome")] string Surname,
        [property: JsonPropertyName("Documento")] string Document,
        [property: JsonPropertyName("Departamento")] string Department,
        [property: JsonPropertyName("SalarioBruto")] decimal GrossSalary,
        [property: JsonPropertyName("DataAdmissao")] DateTime AdmissionDate,
        [property: JsonPropertyName("PossuiDescontoPlanoSaude")] bool HasHealthInsuranceDeduction,
        [property: JsonPropertyName("PossuiDescontoPlanoDental")] bool HasDentalPlanDeduction,
        [property: JsonPropertyName("PossuiDescontoValeTransporte")] bool HasTransportationVoucherDeduction
    )
    {
        public GetEmployeeResponse() : this(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            decimal.Zero,
            DateTime.MinValue,
            false,
            false,
            false
            )
        {
        }
    }
}
