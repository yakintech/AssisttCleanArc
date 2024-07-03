using Assistt.Application.Commands;
using Assistt.Application.DTO;
using Assistt.Application.Mapping;
using Assistt.Application.Services.Auth;
using Assistt.Application.Validators.Products;
using Assistt.Infrastructure.EF;
using Assistt.Infrastructure.Extensions;
using Assistt.Infrastructure.Repositories;
using Assistt.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AssisttContext>();

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblyContaining<ProductCommands.CreateProduct>();
    opt.RegisterServicesFromAssemblyContaining<UserCommands.UserLogin>();
});


builder.Services.AddRepositories();

builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();


builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(GetAllProductsProfile).Assembly);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Asist",
        ValidAudience = "Asist",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ironmaidenpentagramslipknotironmaidenpentagramslipknot"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
