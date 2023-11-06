using FluentValidation;
using Stone.Payroll.Application.CustomValidations;

namespace Stone.Payroll.Application.Commands.CreateEmployee
{
    /// <summary>
    /// Validador para o comando de criação de funcionário.
    /// </summary>
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(r => r.Name).NotEmpty().OverridePropertyName("Nome");
            RuleFor(r => r.Surname).NotEmpty().OverridePropertyName("Sobrenome");
            RuleFor(r => r.Document).NotEmpty().SetValidator(new CpfValidator()).OverridePropertyName("Documento");
            RuleFor(r => r.Department).NotEmpty().OverridePropertyName("Setor");
            RuleFor(r => r.GrossSalary).NotEmpty().GreaterThan(0).OverridePropertyName("SalarioBruto");
            RuleFor(r => r.AdmissionDate).NotEmpty().OverridePropertyName("DataAdmissao");
            RuleFor(r => r.HasHealthInsuranceDeduction).NotEmpty().OverridePropertyName("PossuiDescontoPlanoSaude");
            RuleFor(r => r.HasDentalPlanDeduction).NotEmpty().OverridePropertyName("PossuiDescontoPlanoDental");
            RuleFor(r => r.HasTransportationVoucherDeduction).NotEmpty().OverridePropertyName("PossuiDescontoValeTransporte");
        }
    }
}
