using System.ComponentModel;

namespace Stone.Payroll.Utils.Extensions
{
    /// <summary>
    /// Fornece métodos de extensão para enums.
    /// </summary>
    public static class EnumExtesion
    {
        /// <summary>
        /// Obtém a descrição de um enum a partir do valor.
        /// </summary>
        /// <param name="value">O valor do enum.</param>
        /// <returns>A descrição do enum, se estiver presente; caso contrário, o próprio valor do enum como string.</returns>
        public static string GetDescriptionFromEnumValue(this Enum value)
        {
            DescriptionAttribute? attribute = value.GetType()
                .GetField(value.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
