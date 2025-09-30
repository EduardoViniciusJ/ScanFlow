# ScanFlowAWS API

API utilizando **DDD (Domain-Driven Design)** e integração com **AWS Rekognition**.  
Possui autenticação JWT, refresh tokens e endpoints para análise e comparação de faces.

---

## Tecnologias

- .NET 8 / ASP.NET Core Web API
- BCrypt
- EntityFramework
- C#
- AWS Rekognition
- JWT Authentication
- AutoMapper
- FluentValidation
- Swagger (OpenAPI)
- Git/GitHub

---

## Funcionalidades

  - Registro de usuários (`/api/user/register`)
  - Login de usuários (`/api/user/login`)
  - Refresh token (`/api/refreshtoken`)

  - Analisar emoções de faces (`/api/rekognition/analyzefaces`)
  - Comparar faces (`/api/rekognition/comparecefaces`)

  - Validações com FluentValidation
  - Tratamento de erros customizado (Exception Filter)
  - Tradução de emoções.
  - Middleware de cultura para suporte a `Accept-Language`
