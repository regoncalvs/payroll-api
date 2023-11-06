using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy;

namespace Stone.Payroll.Tests.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    public class CalculateInssDeductionStrategyTest
    {
        [Theory]
        [InlineData("João", "Silva", "00000000000", "TI", 1044, "2023-01-01", true, false, false, 0.075)]
        [InlineData("João", "Silva", "00000000000", "TI", 2089.5, "2023-01-01", true, false, false, 0.09)]
        [InlineData("João", "Silva", "00000000000", "TI", 3134.3, "2023-01-01", true, false, false, 0.12)]
        [InlineData("João", "Silva", "00000000000", "TI", 6101.06, "2023-01-01", true, false, false, 0.14)]
        [InlineData("João", "Silva", "00000000000", "TI", 10000, "2023-01-01", true, false, false, 0.14)]
        public void ValidateInssDeduction_WithValidData_ShouldReturnCorrectValueDeduction(string name, string surname, string document, string department, decimal grossSalary, DateTime admissionDate, bool hasHealthInsuranceDeduction, bool hasDentalPlanDeduction, bool hasTransportationVoucherDeduction, decimal correctTier)
        {
            #region Arrange           

            var employee = new Employee(
                Guid.NewGuid(),
                name,
                surname,
                document,
                department,
                grossSalary,
                admissionDate,
                hasHealthInsuranceDeduction,
                hasDentalPlanDeduction,
                hasTransportationVoucherDeduction);

            var strategy = new CalculateInssDeductionStrategy();
            #endregion

            #region Act
            var result = strategy.Calculate(employee);
            #endregion

            #region Assert
            Assert.Equal(result, grossSalary * correctTier);
            #endregion
        }
    }
}
