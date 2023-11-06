using MediatR;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Application.Queries.GetPayStub
{
    /// <summary>
    /// Representa a solicitação para obter o contracheque de um funcionário.
    /// </summary>
    public class GetPayStubQuery : IRequest<GetPayStubResponse>
    {
        /// <summary>
        /// Obtém ou define o Id do funcionário.
        /// </summary>
        [JsonPropertyName("IdFuncionario")]
        public Guid EmployeeId { get; set; }
    }
}
