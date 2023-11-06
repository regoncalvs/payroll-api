using MediatR;

namespace Stone.Payroll.Application.Queries.GetEmployee
{
    /// <summary>
    /// Representa a solicitação para obter um funcionário.
    /// </summary>
    public class GetEmployeeQuery : IRequest<GetEmployeeResponse>
    {
        /// <summary>
        /// Obtém ou define o identificador único do funcionário.
        /// </summary>
        public Guid Id { get; set; }
    }
}
