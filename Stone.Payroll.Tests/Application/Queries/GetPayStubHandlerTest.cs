using Microsoft.EntityFrameworkCore;
using Moq;
using Stone.Payroll.Application.Queries.GetPayStub;
using Stone.Payroll.Infrastructure.Persistence;
using Stone.Payroll.Infrastructure.ReadModels;
using Stone.Payroll.Utils.Exceptions;

namespace Stone.Payroll.Tests.Application.Commands
{
    public class GetPayStubHandlerTest
    {
        [Theory]
        [InlineData("c0785bef-d70e-429a-a599-f59c1404fc15")]
        public void ValidatePayStubQuery_WithValidData_ShouldReturnCorrectPayStub(string employeeId)
        {
            #region Arrange

            Guid guidEmployeeId = Guid.Parse(employeeId);
            decimal grossSalary = 1500;

            var employees = new List<EmployeeRead>
            {
                new EmployeeRead(guidEmployeeId, "Maria", "Souza", "00000000000", "Departamento", grossSalary, DateTime.Now.AddYears(-5), false, true, true),
                new EmployeeRead(Guid.NewGuid(), "João", "Souza", "00000000000", "Departamento", 1000, DateTime.Now.AddYears(-5), false, true, true)
            }.AsQueryable();

            Mock<DbSet<EmployeeRead>> mockSetEmployees = NewMockSet(employees);

            var readContext = new Mock<EmployeesReadContext>();

            readContext
                .Setup(c => c.Employees).Returns(mockSetEmployees.Object);

            var handler = new GetPayStubHandler(readContext.Object);

            var command = new GetPayStubQuery() { EmployeeId = guidEmployeeId };
            #endregion

            #region Act
            var result = handler.Handle(command, new CancellationToken()).Result;
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(result.PayStub.GrossSalary, grossSalary);
            Assert.Equal(result.PayStub.ReferenceMonth, DateTime.Today.ToString("MM/yyyy"));
            #endregion
        }


        [Theory]
        [InlineData("c0785bef-d70e-429a-a599-f59c1404fc15")]
        public async Task ValidatePayStubQuery_WithEmployeeThatDoesNotExist_ShouldThrowExceptionAsync(string employeeId)
        {
            #region Arrange

            Guid guidEmployeeId = Guid.Parse(employeeId);

            var employees = new List<EmployeeRead>
            {
                new EmployeeRead(Guid.NewGuid(), "Maria", "Souza", "00000000000", "Departamento", 1500, DateTime.Now.AddYears(-5), false, true, true),
                new EmployeeRead(Guid.NewGuid(), "João", "Souza", "00000000000", "Departamento", 1000, DateTime.Now.AddYears(-5), false, true, true)
            }.AsQueryable();

            Mock<DbSet<EmployeeRead>> mockSetEmployees = NewMockSet(employees);

            var readContext = new Mock<EmployeesReadContext>();

            readContext
                .Setup(c => c.Employees).Returns(mockSetEmployees.Object);

            var handler = new GetPayStubHandler(readContext.Object);

            var command = new GetPayStubQuery() { EmployeeId = guidEmployeeId };
            #endregion

            #region Act
            var exception = await Record.ExceptionAsync(async () => await handler.Handle(command, CancellationToken.None));
            #endregion

            #region Assert
            Assert.IsType<NotFoundException>(exception);
            #endregion
        }
        private static Mock<DbSet<T>> NewMockSet<T>(IQueryable<T> employees) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => employees.GetEnumerator());
            return mockSet;
        }
    }
}
