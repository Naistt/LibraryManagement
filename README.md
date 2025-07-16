
# 📚 Library Management System

Sistema Full Stack para gerenciamento de **Gêneros**, **Autores** e **Livros**. Este projeto foi desenvolvido como parte de um desafio técnico, seguindo boas práticas de arquitetura e organização de código tanto no backend (.NET 6 + PostgreSQL) quanto no frontend (Angular + PrimeNG).

## ✅ Funcionalidades

- Cadastro, edição, exclusão e visualização de Gêneros, Autores e Livros
- Relacionamentos:
  - Um **Gênero** possui N **Livros**
  - Um **Autor** possui N **Livros**
  - Cada **Livro** pertence a apenas um **Autor** e um **Gênero**
- Exibição dos livros relacionados diretamente na listagem de gêneros
- Interface responsiva com uso de **PrimeNG**
- Requisições com feedback ao usuário via **PrimeNG Toast**
- Confirmações com **PrimeNG ConfirmDialog**

## 🧠 Tecnologias Utilizadas

### Backend (.NET 6 + PostgreSQL)

- ASP.NET Core Web API
- Entity Framework Core (ORM)
- PostgreSQL
- Migrations
- Swagger (documentação)
- DTOs, ViewModels e Respostas padronizadas (BaseResponse)
- Testes de unidade com xUnit, Moq e FluentAssertions
- Suporte a múltiplos environments via `appsettings.*.json`

### Frontend (Angular 17 + PrimeNG)

- Angular Standalone Components
- PrimeNG para UI/UX
- Reactive Forms
- Gerenciamento de estado com **Services e Observables**
- Rotas
- Interceptors para tratamento de requisições
- Estrutura limpa e escalável

## 🚀 Como Executar o Projeto

### Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- PostgreSQL 12+

### 🗄️ Banco de Dados

Configure sua conexão no arquivo `appsettings.Development.json` (API):

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=YOUR_PASSWORD"
}
```

Rode as migrations:

```bash
cd src/Library.Api
dotnet ef database update
```

### 🔧 Backend

```bash
cd src/Library.Api
dotnet run
```

Acesse a documentação Swagger:
```
https://localhost:7258/swagger
```

### 🌐 Frontend

```bash
cd frontend
npm install
npm start
```
Ou vá até a pasta raiz do Angular e execute:
```bash
ng s -o
``` 

A aplicação estará disponível por padrão em:
```
http://localhost:4200
```

## 🧪 Testes de Unidade

Execute os testes com:

```bash
cd src/Library.Application.Tests
dotnet test
```

## 📂 Estrutura do Projeto

```bash
library-management/
├── backend/
│   ├── Library.Api/                 # API principal
│   ├── Library.Application/         # Regras de negócio (Services, DTOs, ViewModels)
│   ├── Library.Domain/              # Entidades de domínio
│   ├── Library.Infrastructure/      # Banco de dados, Migrations, Repositórios
│   └── Library.Application.Tests/   # Testes de unidade
├── frontend/                        # Projeto Angular com PrimeNG
```

## ✅ Checklist do Desafio

### Regras de Negócio
- [x] Um gênero pode possuir N livros
- [x] Um autor pode possuir N livros
- [x] Cada livro pertence a apenas um autor e um gênero

### Banco de Dados
- [x] PostgreSQL

### Backend
- [x] CRUD completo (gêneros, autores, livros)
- [x] Boas práticas (SOLID, DI, SRP)
- [x] Versionamento da API (v1)
- [x] Swagger
- [x] Respostas padronizadas
- [x] Environments
- [x] DTOs e ViewModels
- [x] Entidades e ORM
- [x] Migrations
- [x] Testes de unidade

### Frontend
- [x] SPA Angular com PrimeNG
- [x] CRUD completo
- [x] Boas práticas de componentes
- [x] Gerenciamento de estado com services
- [x] Rotas, models, services, interfaces
- [x] Environments e interceptors
- [x] Testes de unidade

### Entrega
- [x] Projeto hospedado no GitHub com instruções claras

### Possíveis Melhorias
- [ ] Gerenciamento de estado mais robusto com NgRx
- [ ] Aumento na quantidade de Testes unitários
- [ ] Docker & Kubernetes
- [ ] Cloud Deploy

## 📫 Contato

Para dúvidas, sugestões ou contribuições, entre em contato via [GitHub Issues](https://github.com/Naistt/LibraryManagementApi/issues).
