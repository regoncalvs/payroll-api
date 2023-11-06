using Stone.Payroll.Domain.Entities;

namespace Stone.Payroll.Domain.Events
{
    /// <summary>
    /// Representa o evento de criação de um funcionário.
    /// </summary>
    public class EmployeeCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeeCreatedEvent"/>.
        /// </summary>
        /// <param name="employee">O funcionário criado.</param>
        public EmployeeCreatedEvent(Employee employee)
        {
            Employee = employee;
        }

        /// <summary>
        /// Obtém o funcionário criado.
        /// </summary>
        public Employee Employee { get; }
    }
}
