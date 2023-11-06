using FluentValidation;
using FluentValidation.Validators;
using Stone.Payroll.Application.Commands.CreateEmployee;
using System.Text.RegularExpressions;

namespace Stone.Payroll.Application.CustomValidations
{
    /// <summary>
    /// Validador para o CPF no comando de criação de funcionário.
    /// </summary>
    public class CpfValidator : PropertyValidator<CreateEmployeeCommand, string>
    {
        public override string Name => "CpfValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "CPF inválido.";
        }

        public override bool IsValid(ValidationContext<CreateEmployeeCommand> context, string cpf)
        {
            return ValidateCpf(cpf);
        }

        private static bool ValidateCpf(string cpf)
        {
            if (HasValidSizeAndDigits(cpf)) return false;

            if (HasAllDigitsEqual(cpf)) return false;

            int sum = CalculateWeightedSum(cpf);
            int ten = CalculateCheckDigit(sum);
            if (ten != int.Parse(cpf.Substring(9, 1)))
            {
                return false;
            }

            sum = CalculateWeightedSumWithTen(cpf);
            int unity = CalculateCheckDigit(sum);
            if (unity != int.Parse(cpf.Substring(10, 1)))
            {
                return false;
            }

            return true;
        }

        private static int CalculateCheckDigit(int sum)
        {
            int digit = 0;
            int remainder = sum % 11;
            if (remainder != 0 && remainder != 1)
            {
                digit = 11 - remainder;
            }

            return digit;
        }

        private static int CalculateWeightedSumWithTen(string cpf)
        {
            int sum = 0;
            for (int i = 1; i <= 10; i++)
            {
                sum += int.Parse(cpf.Substring(i - 1, 1)) * (12 - i);
            }

            return sum;
        }

        private static int CalculateWeightedSum(string cpf)
        {
            int sum = 0;
            for (int i = 1; i <= 9; i++)
            {
                sum += int.Parse(cpf.Substring(i - 1, 1)) * (11 - i);
            }

            return sum;
        }

        private static bool HasValidSizeAndDigits(string cpf)
        {
            return cpf.Length < 11 || !Regex.IsMatch(cpf, @"^\d{11}$");
        }

        private static bool HasAllDigitsEqual(string cpf)
        {
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
