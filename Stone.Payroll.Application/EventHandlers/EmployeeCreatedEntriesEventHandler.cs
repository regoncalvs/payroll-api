using MediatR;
using Microsoft.Extensions.Logging;
using Stone.Payroll.Utils.Extensions;
using Stone.Payroll.Domain.Events;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Infrastructure.ReadModels;

namespace Stone.Payroll.Application.EventHandlers
{
    /// <summary>
    /// Manipulador de eventos para calcular descontos e inserir lançamentos.
    /// </summary>
    public class EmployeeCreatedEntriesEventHandler : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly EmployeesReadContext _readContext;
        private readonly ILogger<EmployeeCreatedEntriesEventHandler> _logger;
        private readonly IEnumerable<ICalculateEntry> _calculateEntryStrategies;

        public EmployeeCreatedEntriesEventHandler(
            EmployeesReadContext context,
            ILogger<EmployeeCreatedEntriesEventHandler> logger,
            IEnumerable<ICalculateEntry> calculateEntryStrategies)
        {
            _readContext = context;
            _logger = logger;
            _calculateEntryStrategies = calculateEntryStrategies;
        }

        /// <summary>
        /// Manipula o evento de criação de funcionário para calcular descontos e inserir lançamentos.
        /// </summary>
        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var employee = notification.Employee;
                foreach (var strategy in _calculateEntryStrategies)
                {
                    var deductionValue = strategy.Calculate(employee);
                    _readContext.Entries.Add(new Entry(strategy.EntryType.GetDescriptionFromEnumValue(), deductionValue, strategy.Description, employee.Id));
                }
                await _readContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Lançamentos inseridos na base de leitura com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir lançamentos na base de leitura.");
                throw;
            }
        }
    }
}
