using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stone.Payroll.Infrastructure.ReadModels;

namespace Stone.Payroll.Infrastructure.Persistence
{
    /// <summary>
    /// Contexto para acesso aos dados de leitura de funcionários.
    /// </summary>
    public class EmployeesReadContext : DbContext
    {
        private readonly ILogger<EmployeesReadContext> _logger;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeesReadContext"/>.
        /// </summary>
        public EmployeesReadContext()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeesReadContext"/>.
        /// </summary>
        /// <param name="options">As opções de configuração do contexto.</param>
        /// <param name="logger">O logger para o contexto.</param>
        public EmployeesReadContext(
            DbContextOptions<EmployeesReadContext> options,
            ILogger<EmployeesReadContext> logger) : base(options)
        {
            _logger = logger;
        }

        /// <summary>
        /// Configura o comportamento de carregamento preguiçoso.
        /// </summary>
        /// <param name="optionsBuilder">O construtor de opções de contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Obtém ou define os funcionários no contexto de leitura.
        /// </summary>
        public virtual DbSet<EmployeeRead> Employees => Set<EmployeeRead>();

        /// <summary>
        /// Obtém ou define os lançamentos no contexto de leitura.
        /// </summary>
        public virtual DbSet<Entry> Entries => Set<Entry>();

        /// <summary>
        /// Configura o relacionamento entre funcionários e lançamentos.
        /// </summary>
        /// <param name="modelBuilder">O construtor de modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeRead>()
            .HasMany(l => l.Entries)
            .WithOne()
            .HasForeignKey(l => l.EmployeeReadId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
