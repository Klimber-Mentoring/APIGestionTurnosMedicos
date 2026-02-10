using APIGestionTurnosMedicos;
using APIGestionTurnosMedicos.Auth;
using APIGestionTurnosMedicos.Helpers; 
using APIGestionTurnosMedicos.Models.Repositories;
using APIGestionTurnosMedicos.Models.Repositories.Impl;
using APIGestionTurnosMedicos.Servicies;
using APIGestionTurnosMedicos.Servicies.Impl;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });

var configAutomapper = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}, loggerFactory);

var mapper = configAutomapper.CreateMapper();
builder.Services.AddSingleton(mapper);


// Add services to the container.
builder.Services.AddSingleton<Inicializador>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddSingleton<IAppointmentService, AppointmentService>();

builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddSingleton<IDoctorService, DoctorService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>(); // .NET se encarga de pasarle el Logger solo
});

// SWAGGER
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Documento principal
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Appointments API", Version = "v1" });

    // Comentarios XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // Seguridad básica
    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer Scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

// AUTHENTICATION

builder.Services.AddAuthentication("Authentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Authentication", null);


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7041, o => o.UseHttps());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var inicializador = scope.ServiceProvider.GetRequiredService<Inicializador>();
    inicializador.CargarDatosPrueba();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

