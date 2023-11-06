using MediatR;
using Microsoft.EntityFrameworkCore;
using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Utils.Exceptions;

namespace Stone.Payroll.Application.Queries.GetPayStub
{
    /// <summary>
    /// Manipula a solicitação para obter o contracheque de um funcionário.
    /// </summary>
    public class GetPayStubHandler : IRequestHandler<GetPayStubQuery, GetPayStubResponse>
    {
        private readonly EmployeesReadContext _readContext;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetPayStubHandler"/>.
        /// </summary>
        /// <param name="readContext">O contexto de leitura dos funcionários.</param>
        public GetPayStubHandler(EmployeesReadContext readContext)
        {
            _readContext = readContext;
        }

        /// <summary>
        /// Manipula a solicitação para obter o contracheque de um funcionário.
        /// </summary>
        /// <param name="request">A solicitação para obter o contracheque.</param>
        /// <param name="cancellationToken">O token de cancelamento.</param>
        /// <returns>O contracheque do funcionário obtido.</returns>
        public async Task<GetPayStubResponse> Handle(GetPayStubQuery request, CancellationToken cancellationToken)
        {
            var employee = _readContext.Employees.Include(f => f.Entries)
                .FirstOrDefault(f => f.Id == request.EmployeeId)
                ?? throw new NotFoundException($"Funcionário com o Id {request.EmployeeId} não encontrado.");

            var payStub = new PayStub(employee);

            return new GetPayStubResponse(payStub);
        }
    }
}
