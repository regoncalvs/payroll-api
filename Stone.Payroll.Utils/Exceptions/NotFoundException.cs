namespace Stone.Payroll.Utils.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um item não é encontrado.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="NotFoundException"/>.
        /// </summary>
        /// <param name="message">A mensagem de erro.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
