using Microsoft.AspNetCore.Identity;
using ScanFlowAWS.API.Filters;
using ScanFlowAWS.Application;
using ScanFlowAWS.Application.Services;
using ScanFlowAWS.Application.UseCases.User.Register;
using ScanFlowAWS.Infrastructure;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AutoMapping());
});
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication();
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
