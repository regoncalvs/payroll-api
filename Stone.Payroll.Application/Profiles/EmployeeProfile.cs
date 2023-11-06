using AutoMapper;
using Stone.Payroll.Application.Queries.GetEmployee;
using Stone.Payroll.Domain.Entities;
using Stone.Payroll.Infrastructure.ReadModels;

namespace FolhaSalarialAPI.Domain.Profiles
{
    /// <summary>
    /// Perfil para mapeamento de propriedades entre entidades e objetos de leitura de funcionários.
    /// </summary>
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeeProfile"/>.
        /// Configura mapeamentos entre entidades e objetos de leitura de funcionários.
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeRead>();
            CreateMap<EmployeeRead, GetEmployeeResponse>();
        }
    }
}
