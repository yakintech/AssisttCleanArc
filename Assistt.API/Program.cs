using Assistt.Application.Commands;
using Assistt.Application.DTO;
using Assistt.Application.Mapping;
using Assistt.Application.Validators.Products;
using Assistt.Infrastructure.EF;
using Assistt.Infrastructure.Repositories;
using Assistt.Infrastructure.UnitOfWork;
using FluentValidation;
using FluentValidation.AspNetCore;

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
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(GetAllProductsProfile).Assembly);


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
