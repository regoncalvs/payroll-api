using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stone.Payroll.Application.Commands.CreateEmployee;
using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Utils.Exceptions;
using System.Net.Sockets;
using System.Reflection.Metadata;

namespace Stone.Payroll.Tests.Application.Commands
{
    public class CreateEmployeeHandlerTest
    {
        [Theory]
        [InlineData("João", "Silva", "00000000000", "TI", 1000, "2023-01-01", true, false, false)]
        public void ValidateEmployeeCommand_WithValidData_ShouldPass(string name, string surname, string document, string department, decimal grossSalary, DateTime admissionDate, bool hasHealthInsuranceDeduction, bool hasDentalPlanDeduction, bool hasTransportationVoucherDeduction)
        {
            #region Arrange
            var validator = new Mock<IValidator<CreateEmployeeCommand>>();
            var writeContext = new Mock<EmployeesWriteContext>();
            var mockSet = new Mock<DbSet<Employee>>();

            writeContext
                .Setup(c => c.Employees).Returns(mockSet.Object);

            validator
                .Setup(v => v.Validate(It.IsAny<CreateEmployeeCommand>()))
                .Returns(new ValidationResult());

            var handler = new CreateEmployeeHandler(writeContext.Object, validator.Object);

            var command = new CreateEmployeeCommand(
                name,
                surname,
                document,
                department,
                grossSalary,
                admissionDate,
                hasHealthInsuranceDeduction,
                hasDentalPlanDeduction,
                hasTransportationVoucherDeduction);
            #endregion

            #region Act
            var result = handler.Handle(command, new CancellationToken()).Result;
            #endregion

            #region Assert
            Assert.NotNull(result);
            validator.Verify(v => v.Validate(It.IsAny<CreateEmployeeCommand>()), Times.Once);
            #endregion
        }

        [Theory]
        [InlineData("João", "Silva", "abc", "TI", 1000, "2023-01-01", true, false, false)]
        public async Task ValidateEmployeeCommand_WithInvalidData_ShouldThrowExceptionAsync(string name, string surname, string document, string department, decimal grossSalary, DateTime admissionDate, bool hasHealthInsuranceDeduction, bool hasDentalPlanDeduction, bool hasTransportationVoucherDeduction)
        {
            #region Arrange
            var validator = new Mock<IValidator<CreateEmployeeCommand>>();
            var writeContext = new Mock<EmployeesWriteContext>();
            var mockSet = new Mock<DbSet<Employee>>();

            writeContext
                .Setup(c => c.Employees).Returns(mockSet.Object);

            var errors = new List<ValidationFailure>
            {
                new ValidationFailure("Document", "Erro 1"),
                new ValidationFailure("Nome", "Erro 2")
            };

            validator
                .Setup(v => v.Validate(It.IsAny<CreateEmployeeCommand>()))
                .Returns(new ValidationResult(errors));

            var handler = new CreateEmployeeHandler(writeContext.Object, validator.Object);

            var command = new CreateEmployeeCommand(
                name,
                surname,
                document,
                department,
                grossSalary,
                admissionDate,
                hasHealthInsuranceDeduction,
                hasDentalPlanDeduction,
                hasTransportationVoucherDeduction);
            #endregion

            #region Act
            var exception = await Record.ExceptionAsync(async () => await handler.Handle(command, CancellationToken.None));
            #endregion

            #region Assert
            Assert.IsType<ValidationFailedException>(exception);            
            validator.Verify(v => v.Validate(It.IsAny<CreateEmployeeCommand>()), Times.Once);
            #endregion
        }
    }
}
