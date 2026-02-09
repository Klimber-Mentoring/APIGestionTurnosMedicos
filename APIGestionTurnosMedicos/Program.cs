using APIGestionTurnosMedicos.Helpers;
using APIGestionTurnosMedicos.Models.Repositories;
using APIGestionTurnosMedicos.Servicies;
using APIGestionTurnosMedicos.Servicies.Impl;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });

var configAutomapper = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}, loggerFactory);

var mapper = configAutomapper.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aContka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>(); // .NET se encarga de pasarle el Logger solo
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


