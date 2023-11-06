using Stone.Payroll.Domain.Events;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stone.Payroll.Domain.Entities
{
    /// <summary>
    /// Classe que representa um funcionário.
    /// </summary>
    public class Employee : IHasDomainEvent
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Employee"/>.
        /// </summary>
        public Employee(
            Guid id,
            string name,
            string surname,
            string document,
            string department,
            decimal grossSalary,
            DateTime admissionDate,
            bool hasHealthInsuranceDeduction,
            bool hasDentalPlanDeduction,
            bool hasTransportationVoucherDeduction)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Document = document;
            Department = department;
            GrossSalary = grossSalary;
            AdmissionDate = admissionDate;
            HasHealthInsuranceDeduction = hasHealthInsuranceDeduction;
            HasDentalPlanDeduction = hasDentalPlanDeduction;
            HasTransportationVoucherDeduction = hasTransportationVoucherDeduction;

            DomainEvents.Add(new EmployeeCreatedEvent(this));
        }

        /// <summary>
        /// Obtém ou define o identificador do funcionário.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do funcionário.
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Obtém ou define o sobrenome do funcionário.
        /// </summary>
        [Required]
        public string Surname { get; private set; }

        /// <summary>
        /// Obtém ou define o documento do funcionário.
        /// </summary>
        [Required]
        public string Document { get; private set; }

        /// <summary>
        /// Obtém ou define o departamento do funcionário.
        /// </summary>
        [Required]
        public string Department { get; private set; }

        /// <summary>
        /// Obtém ou define o salário bruto do funcionário.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GrossSalary { get; private set; }

        /// <summary>
        /// Obtém ou define a data de admissão do funcionário.
        /// </summary>
        [Required]
        public DateTime AdmissionDate { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano de saúde.
        /// </summary>
        [Required]
        public bool HasHealthInsuranceDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano dental.
        /// </summary>
        [Required]
        public bool HasDentalPlanDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no vale transporte.
        /// </summary>
        [Required]
        public bool HasTransportationVoucherDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define a lista de eventos de domínio do funcionário.
        /// </summary>
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
