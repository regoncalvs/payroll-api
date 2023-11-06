using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy;

namespace Stone.Payroll.Tests.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    public class CalculateHealthInsuranceDeductionStrategyTest
    {
        [Theory]
        [InlineData("João", "Silva", "00000000000", "TI", 1044, "2023-01-01", true, false, true)]
        [InlineData("João", "Silva", "00000000000", "TI", 2089.5, "2023-01-01", true, true, false)]
        public void ValidateHealthInsuranceDeduction_WithHealthInsurance_ShouldReturnFixedDeduction(string name, string surname, string document, string department, decimal grossSalary, DateTime admissionDate, bool hasHealthInsuranceDeduction, bool hasDentalPlanDeduction, bool hasTransportationVoucherDeduction)
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

            var strategy = new CalculateHealthInsuranceDeductionStrategy();
            #endregion

            #region Act
            var result = strategy.Calculate(employee);
            #endregion

            #region Assert
            Assert.Equal(10, result);
            #endregion
        }
        
        [Theory]
        [InlineData("João", "Silva", "00000000000", "TI", 1044, "2023-01-01", false, false, true)]
        [InlineData("João", "Silva", "00000000000", "TI", 2089.5, "2023-01-01", false, true, false)]
        public void ValidateHealthInsuranceDeduction_WithoutHealthInsurance_ShouldReturnZero(string name, string surname, string document, string department, decimal grossSalary, DateTime admissionDate, bool hasHealthInsuranceDeduction, bool hasDentalPlanDeduction, bool hasTransportationVoucherDeduction)
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

            var strategy = new CalculateHealthInsuranceDeductionStrategy();
            #endregion

            #region Act
            var result = strategy.Calculate(employee);
            #endregion

            #region Assert
            Assert.Equal(0, result);
            #endregion
        }
    }
}
