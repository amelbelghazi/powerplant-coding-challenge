using FluentValidation;
using FluentValidation.AspNetCore;
using ProductionPlanAPI.Dtos.Request;
using ProductionPlanAPI.Helpers;
using ProductionPlanAPI.Middlewares;
using ProductionPlanAPI.Services;
using ProductionPlanAPI.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductionPlanService, ProductionPlanService>();
builder.Services.AddSingleton<PowerplantCreator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateProductionPlanRequest>, CreateProductionPlanValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
