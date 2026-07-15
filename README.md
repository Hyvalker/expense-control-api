# ExpenseControl

![.NET](https://img.shields.io/badge/.NET-10-purple)
![React](https://img.shields.io/badge/React-19-61DAFB)
![TypeScript](https://img.shields.io/badge/TypeScript-5-blue)
![SQLite](https://img.shields.io/badge/SQLite-Database-003B57)

Projeto desenvolvido como solução para um desafio técnico de desenvolvimento Full Stack.

Sistema de controle de gastos residenciais desenvolvido em **.NET 10 + React + TypeScript**, utilizando os princípios de **Clean Architecture** e boas práticas de desenvolvimento.

A aplicação permite o gerenciamento de pessoas e transações financeiras, aplicando regras de negócio específicas e apresentando relatórios consolidados de receitas, despesas e saldo por pessoa e de forma geral.

## ✨ Funcionalidades

- Cadastro, listagem e exclusão de pessoas
- Cadastro, listagem e exclusão de receitas e despesas (transações)
- Exclusão automática das transações ao remover uma pessoa
- Persistência local utilizando SQLite
- Validação de regras de negócio
- Relatórios financeiros individuais e consolidados
- Documentação interativa da API com Swagger
- Testes unitários implementados para validação de regras de negócio

## 📂 Estrutura do Projeto

```text
ExpenseControl
│
├── ExpenseControl.API          # API REST
├── ExpenseControl.Core         # Regras de negócio
├── ExpenseControl.Infrastructure # Persistência de dados
├── ExpenseControl.Tests        # Projeto de testes unitários
├── frontend                    # React + TypeScript
└── ExpenseControl.sln
```

## 🏛 Arquitetura

O projeto foi estruturado utilizando uma separação em camadas seguindo os princípios da Clean Architecture.

### ExpenseControl.Core

Contém:

- Entidades
- Interfaces
- Modelos
- Regras de negócio
- Serviços

### ExpenseControl.Infrastructure

Responsável por:

- Entity Framework Core
- SQLite
- Repositórios
- Migrações

### ExpenseControl.API

Responsável por:

- Controllers
- DTOs
- Configuração da aplicação
- Swagger
- Middleware

### frontend

Aplicação React contendo:

- Components
- Pages
- Hooks
- Services

## 🧪 Testes Unitários

O projeto inclui testes unitários desenvolvidos com xUnit e Moq, focados na garantia da integridade das regras de negócio críticas, como a validação de restrição de cadastro de receitas para menores de 18 anos.


## 📌 Regras de Negócio

O sistema implementa as seguintes regras:

- Pessoas menores de 18 anos podem cadastrar apenas despesas.
- Ao remover uma pessoa, todas as suas transações são removidas automaticamente.
- Toda transação deve estar vinculada a uma pessoa existente.
- Os relatórios apresentam receitas, despesas e saldo individual, além do consolidado geral.

## 💾 Persistência

Foi utilizado SQLite como banco de dados da aplicação.

A escolha foi feita por permitir que o projeto seja executado imediatamente, sem necessidade de instalação ou configuração de um servidor de banco de dados, facilitando a avaliação da solução.

## 🚀 Executando o projeto

### Pré-requisitos

- .NET SDK 10
- Node.js 18+

---

### Clone o repositório

```bash
git clone https://github.com/Hyvalker/expense-control-api.git
```

---

### Backend

```bash
cd expense-control-api
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update --project ExpenseControl.Infrastructure --startup-project ExpenseControl.API
dotnet run --project ExpenseControl.API
```

A API será iniciada em:

```
http://localhost:5117
```

Documentação da API (Swagger):

```
http://localhost:5117/swagger
```

---

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Aplicação:

```
http://localhost:5173
```

## 🛠 Tecnologias

| Tecnologia            | Finalidade              |
|-----------------------|-------------------------|
| .NET 10               | Backend                 |
| ASP.NET Core          | API REST                |
| Entity Framework Core | ORM                     |
| SQLite                | Banco de Dados          |
| Swagger               | Documentação            |
| xUnit                 | Testes unitários        |
| Moq                   | Mocking de dependências |
| React                 | Frontend                |
| TypeScript            | Tipagem                 |
| Axios                 | Comunicação HTTP        |
| Vite                  | Build Tool              |
| Tailwind CSS          | Estilização             |

## 📚 Documentação

Com o objetivo de facilitar a manutenção e compreensão do projeto, o código foi documentado utilizando:

- XML Documentation no backend;
- JSDoc no frontend;
- Swagger/OpenAPI para documentação dos endpoints.

## 💡 Decisões Técnicas

Durante o desenvolvimento foram adotadas algumas decisões visando simplicidade, escalabilidade e facilidade de avaliação:

- Utilização de Clean Architecture para separação de responsabilidades;
- Persistência em SQLite para evitar dependência de bancos externos;
- Utilização de DTOs para isolamento da camada de API;
- Repository Pattern para acesso aos dados;
- Documentação automática com Swagger;
- Código documentado com XML Comments e JSDoc;
- Injeção de dependências utilizando o container nativo do ASP.NET Core.

## 📝 Sobre o projeto

Este projeto foi desenvolvido como parte de um desafio técnico, buscando demonstrar boas práticas de arquitetura, organização de código, documentação e implementação de regras de negócio utilizando .NET e React.

## 🚧 Melhorias Futuras

- Implementação de autenticação
- Edição de transações
- Filtros e ordenação nas consultas
- Containerização com Docker
- Expansão da cobertura de testes unitários
