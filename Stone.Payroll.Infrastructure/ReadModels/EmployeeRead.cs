using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Infrastructure.ReadModels
{
    /// <summary>
    /// Representa um funcionário no contexto de leitura.
    /// </summary>
    public class EmployeeRead
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeeRead"/>.
        /// </summary>
        public EmployeeRead(
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
        }

        /// <summary>
        /// Obtém ou define o Id do funcionário.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("Nome")]
        public string Name { get; private set; }

        /// <summary>
        /// Obtém ou define o sobrenome do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("Sobrenome")]
        public string Surname { get; private set; }

        /// <summary>
        /// Obtém ou define o documento do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("Documento")]
        public string Document { get; private set; }

        /// <summary>
        /// Obtém ou define o departamento do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("Departamento")]
        public string Department { get; private set; }

        /// <summary>
        /// Obtém ou define o salário bruto do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("SalarioBruto")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GrossSalary { get; private set; }

        /// <summary>
        /// Obtém ou define a data de admissão do funcionário.
        /// </summary>
        [Required]
        [JsonPropertyName("DataAdmissao")]
        public DateTime AdmissionDate { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano de saúde.
        /// </summary>
        [Required]
        [JsonPropertyName("PossuiDescontoPlanoSaude")]
        public bool HasHealthInsuranceDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no plano dental.
        /// </summary>
        [Required]
        [JsonPropertyName("PossuiDescontoPlanoDental")]
        public bool HasDentalPlanDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define se o funcionário possui desconto no vale transporte.
        /// </summary>
        [Required]
        [JsonPropertyName("PossuiDescontoValeTransporte")]
        public bool HasTransportationVoucherDeduction { get; private set; }

        /// <summary>
        /// Obtém ou define os lançamentos do funcionário.
        /// </summary>
        public virtual IEnumerable<Entry>? Entries { get; set; }
    }
}
