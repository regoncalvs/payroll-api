using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Stone.Payroll.Infrastructure.ReadModels
{
    /// <summary>
    /// Representa uma lançamento no contexto de leitura.
    /// </summary>
    public class Entry
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Entry"/>.
        /// </summary>
        public Entry()
        {

        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Entry"/>.
        /// </summary>
        public Entry(
            string type,
            decimal value,
            string description,
            Guid employeeReadId)
        {
            Type = type;
            Value = value;
            Description = description;
            EmployeeReadId = employeeReadId;
        }

        /// <summary>
        /// Obtém ou define o ID do lançamento.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o tipo do lançamento.
        /// </summary>
        [Required]
        [JsonPropertyName("Tipo")]
        public string Type { get; private set; }

        /// <summary>
        /// Obtém ou define o valor do lançamento.
        /// </summary>
        [Required]
        [JsonPropertyName("Valor")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; private set; }

        /// <summary>
        /// Obtém ou define a descrição do lançamento.
        /// </summary>
        [Required]
        [JsonPropertyName("Descricao")]
        public string Description { get; private set; }

        /// <summary>
        /// Obtém ou define o Id do funcionário no contexto de leitura.
        /// </summary>
        [JsonIgnore]
        public Guid? EmployeeReadId { get; set; }
    }
}
