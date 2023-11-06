using FluentValidation;
using Stone.Payroll.Application.Commands.CreateEmployee;
using Stone.Payroll.Application.CustomValidations;

namespace Stone.Payroll.Tests.Domain.Services.CalculateEntry.CalculateDeductionStrategy
{
    public class CpfValidatorTestTest
    {
        [Theory]
        [InlineData("98102776013")]
        [InlineData("62894490011")]
        [InlineData("88558784094")]
        public void ValidateCpfValidation_WithValidCpf_ShouldReturnTrue(string cpf)
        {
            #region Arrange           
            var command = new CreateEmployeeCommand("João", "Silva", cpf, "TI", 1044, DateTime.Now.AddYears(-3), true, true, true);
            var context = new ValidationContext<CreateEmployeeCommand>(command);

            var validator = new CpfValidator();
            #endregion

            #region Act
            var result = validator.IsValid(context, cpf);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("83492702098")]
        [InlineData("60904590030")]
        public void ValidateCpfValidation_WithInvalidCpf_ShouldReturnFalse(string cpf)
        {
            #region Arrange           
            var command = new CreateEmployeeCommand("João", "Silva", cpf, "TI", 1044, DateTime.Now.AddYears(-3), true, true, true);
            var context = new ValidationContext<CreateEmployeeCommand>(command);

            var validator = new CpfValidator();
            #endregion

            #region Act
            var result = validator.IsValid(context, cpf);
            #endregion

            #region Assert
            Assert.False(result);
            #endregion
        }

    }
}
