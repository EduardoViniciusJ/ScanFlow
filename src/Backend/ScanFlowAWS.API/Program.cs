using Microsoft.AspNetCore.Identity;
using ScanFlowAWS.API.Filters;
using ScanFlowAWS.Application;
using ScanFlowAWS.Application.Services;
using ScanFlowAWS.Application.UseCases.AmazonRekognition;
using ScanFlowAWS.Application.UseCases.User.Register;
using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure;
using ScanFlowAWS.Infrastructure.Adapters;
using ScanFlowAWS.Infrastructure.DataAcess.Context;
using ScanFlowAWS.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AutoMapping());
});
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


// Configurar dependências
builder.Services.AddSingleton(new RekognitionService("us-east-1"));
builder.Services.AddScoped<IImagemRekognition, ImageRekognitionAdapter>();
builder.Services.AddScoped<AnalyzeImageUseCase>();



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
