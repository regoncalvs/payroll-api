using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stone.Payroll.Application.Commands.CreateEmployee;
using Stone.Payroll.Application.Queries.GetEmployee;
using Stone.Payroll.Application.Queries.GetPayStub;
using Stone.Payroll.Utils.Exceptions;

namespace FolhaSalarialAPI.Controllers;

/// <summary>
/// Controlador responsável por lidar com operações relacionadas a funcionários.
/// </summary>
[ApiController]
[Route("Funcionario")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EmployeeController"/>.
    /// </summary>
    /// <param name="mediator">O mediador para lidar com as solicitações MediatR.</param>
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Recupera um funcionário pelo seu Id.
    /// </summary>
    /// <param name="id">O Id do funcionário.</param>
    /// <returns>O funcionário recuperado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetEmployeeQuery { Id = id });

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error"
            );
        }
    }

    /// <summary>
    /// Recupera o extrato do contracheque de um funcionário com base no seu Id.
    /// </summary>
    /// <param name="idEmployee">O Id do funcionário.</param>
    /// <returns>O extrato de contracheque do funcionário.</returns>
    [HttpGet("{idEmployee}/contracheque")]
    public async Task<IActionResult> GetPayStubByEmployeeId(Guid idEmployee)
    {
        try
        {
            var result = await _mediator.Send(new GetPayStubQuery { EmployeeId = idEmployee });

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error"
            );
        }
    }

    /// <summary>
    /// Cria um novo funcionário.
    /// </summary>
    /// <param name="command">O comando para criar um funcionário.</param>
    /// <returns>O Id do funcionário criado.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Created($"funcionario/{result.Id}", result);
        }
        catch (ValidationFailedException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error"
            );
        }
    }
}
