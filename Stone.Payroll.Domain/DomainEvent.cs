using MediatR;

namespace Stone.Payroll.Domain
{
    /// <summary>
    /// Interface de marcação para entidades que possuem eventos de domínio.
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// Obtém ou define a lista de eventos de domínio.
        /// </summary>
        public List<DomainEvent> DomainEvents { get; set; }
    }

    /// <summary>
    /// Classe base para eventos de domínio.
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DomainEvent"/>.
        /// </summary>
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Obtém ou define se o evento foi publicado.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Obtém ou define a data de ocorrência do evento.
        /// </summary>
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
