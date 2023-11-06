using FluentValidation;
using MediatR;
using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Utils.Exceptions;

namespace Stone.Payroll.Application.Commands.CreateEmployee
{
    /// <summary>
    /// Manipulador para o comando de criação de funcionário.
    /// </summary>
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly EmployeesWriteContext _writeContext;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateEmployeeHandler"/>.
        /// </summary>
        public CreateEmployeeHandler(EmployeesWriteContext writeContext, IValidator<CreateEmployeeCommand> validator)
        {
            _writeContext = writeContext;
            _validator = validator;
        }

        /// <summary>
        /// Manipula o comando para criar um novo funcionário.
        /// </summary>
        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ValidationFailedException(result.Errors);
            }

            Guid idNewEmployee = Guid.NewGuid();
            var newEmployee = new Employee(idNewEmployee,
                                           request.Name,
                                           request.Surname,
                                           request.Document,
                                           request.Department,
                                           request.GrossSalary!.Value,
                                           request.AdmissionDate!.Value,
                                           request.HasHealthInsuranceDeduction!.Value,
                                           request.HasDentalPlanDeduction!.Value,
                                           request.HasTransportationVoucherDeduction!.Value);

            _writeContext.Employees.Add(newEmployee);

            await _writeContext.SaveChangesAsync(cancellationToken);

            return new CreateEmployeeResponse(newEmployee.Id);
        }
    }
}
