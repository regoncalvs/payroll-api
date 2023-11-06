using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Stone.Payroll.Application.Commands.CreateEmployee;
using Stone.Payroll.Application.EventHandlers;
using Stone.Payroll.Domain.Services.CalculateDeductionStrategy;
using Stone.Payroll.Domain.Services.CalculateEntry.CalculateDeductionStrategy;
using Stone.Payroll.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionStringEmployeesWrite = builder.Configuration.GetConnectionString("EmployeesWriteConnection");
var connectionStringEmployeesRead = builder.Configuration.GetConnectionString("EmployeesReadConnection");

builder.Services.AddDbContext<EmployeesWriteContext>(options => options
    .UseSqlServer(connectionStringEmployeesWrite));
builder.Services.AddDbContext<EmployeesReadContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(connectionStringEmployeesRead));

//Adiciona Handler de replicação de employee
builder.Services.AddScoped<EmployeeCreatedEmployeeReadEventHandler>();

//Adiciona strategies de calculo de lançamentos
builder.Services.AddScoped<ICalculateEntry, CalculateFgtsDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateInssDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateIrrfDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateDentalPlanDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateHealthInsuranceDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateTransportationVoucherDeductionStrategy>();
builder.Services.AddScoped<ICalculateEntry, CalculateRemuneration>();
builder.Services.AddScoped<EmployeeCreatedEntriesEventHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(typeof(CreateEmployeeValidator).Assembly);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly);
});
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var contextWrite = services.GetRequiredService<EmployeesWriteContext>();
    var contextRead = services.GetRequiredService<EmployeesReadContext>();
    if (contextWrite.Database.GetPendingMigrations().Any())
    {
        contextWrite.Database.Migrate();
    }
    if (contextRead.Database.GetPendingMigrations().Any())
    {
        contextRead.Database.Migrate();
    }
}

app.Run();
