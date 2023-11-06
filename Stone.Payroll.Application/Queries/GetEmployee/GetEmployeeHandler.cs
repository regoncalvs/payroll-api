using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Utils.Exceptions;

namespace Stone.Payroll.Application.Queries.GetEmployee
{
    /// <summary>
    /// Manipula a solicitação para obter um funcionário.
    /// </summary>
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeResponse>
    {
        private readonly EmployeesReadContext _readContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetEmployeeHandler"/>.
        /// </summary>
        /// <param name="readContext">O contexto de leitura de funcionários.</param>
        /// <param name="mapper">O mapeador para realizar a conversão de entidades.</param>
        public GetEmployeeHandler(EmployeesReadContext readContext, IMapper mapper)
        {
            _readContext = readContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula a solicitação para obter um funcionário.
        /// </summary>
        /// <param name="request">A solicitação para obter um funcionário.</param>
        /// <param name="cancellationToken">O token de cancelamento.</param>
        /// <returns>Um objeto de resposta com o funcionário obtido.</returns>
        public async Task<GetEmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _readContext.Employees
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken: cancellationToken)
                ?? throw new NotFoundException($"Funcionário com o Id {request.Id} não encontrado.");

            var employeeResponse = _mapper.Map<GetEmployeeResponse>(employee);

            return employeeResponse;
        }
    }
}
