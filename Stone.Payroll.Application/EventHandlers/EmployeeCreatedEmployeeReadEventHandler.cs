using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Stone.Payroll.Domain.Events;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Infrastructure.ReadModels;

namespace Stone.Payroll.Application.EventHandlers
{
    /// <summary>
    /// Manipulador de eventos para replicar funcionário na base de leitura.
    /// </summary>
    public class EmployeeCreatedEmployeeReadEventHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly EmployeesReadContext _readContext;
        private readonly ILogger<EmployeeCreatedEmployeeReadEventHandler> _logger;
        private readonly IMapper _mapper;

        public EmployeeCreatedEmployeeReadEventHandler(
            EmployeesReadContext context,
            ILogger<EmployeeCreatedEmployeeReadEventHandler> logger,
            IMapper mapper)
        {
            _readContext = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Manipula o evento de criação de funcionário para replicar na base de leitura.
        /// </summary>
        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var employee = _mapper.Map<EmployeeRead>(notification.Employee);

                _readContext.Employees.Add(employee);
                await _readContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Funcionário replicado na base de leitura com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao replicar o funcionário na base de leitura.");
                throw;
            }
        }
    }
}
