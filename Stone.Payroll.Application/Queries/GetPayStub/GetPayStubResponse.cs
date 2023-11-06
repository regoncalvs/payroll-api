using Stone.Payroll.Domain.Entities;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Application.Queries.GetPayStub
{
    /// <summary>
    /// Representa a resposta contendo o contracheque de um funcionário.
    /// </summary>
    public record GetPayStubResponse([property: JsonPropertyName("Contracheque")] PayStub PayStub);
}
