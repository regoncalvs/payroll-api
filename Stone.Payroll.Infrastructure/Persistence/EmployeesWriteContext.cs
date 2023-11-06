using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stone.Payroll.Domain;
using Stone.Payroll.Domain.Entities;

namespace Stone.Payroll.Infrastructure.Persistence
{
    /// <summary>
    /// Contexto para acesso aos dados de escrita de funcionários.
    /// </summary>
    public class EmployeesWriteContext : DbContext
    {
        private readonly IPublisher _publisher;
        private readonly ILogger<EmployeesWriteContext> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeesWriteContext"/>.
        /// </summary>
        public EmployeesWriteContext()
        {

        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeesWriteContext"/>.
        /// </summary>
        /// <param name="options">As opções de configuração do contexto.</param>
        /// <param name="publisher">O publicador de eventos.</param>
        /// <param name="logger">O logger para o contexto.</param>
        public EmployeesWriteContext(
            DbContextOptions<EmployeesWriteContext> options,
            IPublisher publisher,
            ILogger<EmployeesWriteContext> logger) : base(options)
        {
            _publisher = publisher;
            _logger = logger;
        }        

        /// <summary>
        /// Obtém ou define os funcionários no contexto de escrita.
        /// </summary>
        public virtual DbSet<Employee> Employees => Set<Employee>();

        /// <summary>
        /// Salva as alterações no contexto e publica eventos de domínio.
        /// </summary>
        /// <param name="cancellationToken">O token de cancelamento.</param>
        /// <returns>Um número inteiro que representa o número de registros salvos no contexto.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            foreach (var @event in events)
            {
                @event.IsPublished = true;

                _logger.LogInformation("Novo evento de domínio {Event}", @event.GetType().Name);

                await _publisher.Publish(@event, cancellationToken);
            }

            return await base.SaveChangesAsync(cancellationToken); ;
        }

        /// <summary>
        /// Configura o modelo de entidades, ignorando a lista de eventos de domínio.
        /// </summary>
        /// <param name="builder">O construtor de modelo.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>()
                .Ignore(x => x.DomainEvents);
        }
    }
}
