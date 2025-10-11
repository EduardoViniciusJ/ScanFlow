using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ScanFlowAWS.API.Filters;
using ScanFlowAWS.API.Middlewares;
using ScanFlowAWS.Application;
using ScanFlowAWS.Application.DTOs.Responses;
using ScanFlowAWS.Application.Exceptions;
using ScanFlowAWS.Application.Exceptions.ResourcesMassages;
using ScanFlowAWS.Application.Services.AutoMapper;
using ScanFlowAWS.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapping()));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsJsonAsync(
                new ResponseErrorsJson(new InvalidTokenExceptionLogin().Message)
            );
        },
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("accessToken", out var accessToken))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("origins", policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedOrigins:BlazorDev"]!)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();
app.UseCors("origins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
