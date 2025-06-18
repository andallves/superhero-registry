# 🦸‍♂️ SuperHero Registry

Sistema Fullstack para registro, visualização e gerenciamento de super-heróis e seus superpoderes.

## 📚 Índice

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Funcionalidades](#funcionalidades)
- [Backend (ASP.NET Core)](#backend-aspnet-core)
- [Estrutura de Pastas Backend](#estrutura-de-pastas)
- [Frontend (Angular 17)](#frontend-angular-17)
- [Estrutura de Pastas Frontend](#estrutura-de-pastas)
- [Como Rodar o Projeto](#como-rodar-o-projeto)
- [Scripts SQL](#scripts-sql)
- [Autor](#autor)

---

## 📖 Descrição

Este projeto tem como objetivo cadastrar super-heróis e seus respectivos superpoderes, com possibilidade de criar, editar, listar e excluir. Ele segue boas práticas de arquitetura como **CQRS**, separação de camadas, e uso de banco de dados **PostgreSQL**.

---

## 🛠 Tecnologias Utilizadas

### Backend
- ASP.NET Core 8
- CQRS Pattern
- Entity Framework Core
- PostgreSQL
- AutoMapper
- MediatR

### Frontend
- Angular 17 (Standalone Components)
- Angular Material
- RxJS
- TypeScript
- SCSS

---

## ✅ Funcionalidades

- 🔍 Listar heróis
- ➕ Cadastrar herói e seus poderes
- ✏️ Editar informações do herói
- ❌ Remover herói
- ⚡ Associar múltiplos superpoderes a um herói

---

## 🧠 Backend (ASP.NET Core)

### Estrutura

- `SuperHero.API`: Camada de apresentação (controllers)
- `SuperHero.Application`: Handlers e DTOs
- `SuperHero.Domain`: Entidades e interfaces
- `SuperHero.Infra`: Repositórios, contexto e persistência

### Entidades Principais

```csharp
public class Heroi
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeHeroi { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; } = null;
    public float Altura { get; set; }
    public float Peso { get; set; }
    public List<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = [];
}

public class SuperPoder
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

public class HeroiSuperPoder
{
    public int HeroiId { get; set; }
    public Heroi Heroi { get; set; }

    public int SuperPoderId { get; set; }
    public SuperPoder SuperPoder { get; set; }
}
```

### 🔄 Fluxo de Dados

O sistema utiliza **CQRS (Command Query Responsibility Segregation)**:

- **Commands**: Criar, atualizar, deletar.
- **Queries**: Listar todos, buscar por ID.

Handlers processam cada comando ou consulta, isolando as responsabilidades.

### 💾 Banco de Dados

- **SGBD**: PostgreSQL
- **ORM**: Entity Framework Core
- **Migrations**: Utilizadas para versionar o schema
- **Script de inicialização**: [`scripts/init.sql`](./scripts/init.sql)

---

## 🧠 Frontend (Angular 17)
### Estrutura
```
src/
└── app/
    ├── core/
    │   ├── pipes/         ← Pipes reutilizáveis (ex: formatações de texto, filtros visuais)
    │   ├── services/      ← Serviços globais para requisições HTTP, storage, helpers etc.
    │   └── types/         ← Tipagens e interfaces usadas em todo o app
    │
    ├── template/
    │   └── layout/
    │       ├── filter/    ← Filtro visual de heróis (ex: por nome, poder)
    │       └── navbar/    ← Componente da barra de navegação principal
    │
    ├── shared/
    │   └── components/    ← Componentes reutilizáveis em várias partes do sistema
    │       ├── hero-card/ ← Cartão de exibição de herói
    │       ├── hero-modal/← Modal para criar/editar heróis
    │       ├── loading/   ← Indicador de carregamento
    │       └── result/    ← Exibição de resultado (pode ser feedback, erro, etc.)
    │
    ├── app.component.ts         ← Componente raiz do Angular
    ├── app.component.html       ← Template do componente raiz
    ├── app.component.scss       ← Estilos globais do componente raiz
    ├── app.component.spec.ts    ← Teste unitário do componente raiz
    ├── app.config.ts            ← Configurações globais da aplicação (ex: rotas standalone)
    └── app.routes.ts            ← Definição das rotas da aplicação

```
---

## 📦 Como Executar Frontend (Instalação )

1. Clone o repositório:
   ```bash
   git clone https://github.com/andallves/superhero-registry.git
   cd superher-registry/superhero-registry-ux
   
2. Instale as dependências:
   ```bash
   npm install

3. Execute o projeto localmente:
   ```bash
    ng serve
    Acesse no navegador: http://localhost:4200

## ▶️ Como Executar Backend

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/)


### 1. Clone o repositório
1. Clone o repositório:
   ```bash
   git clone https://github.com/andallves/superhero-registry.git
   cd superher-registry/superhero-registry-api
   
2. Configure o banco:
   ```bash
    Vá na camada de API, no UserSecrets e adicione
    "ConnectionStrings:POSTGRES": "Host=localhost;Port=5432;Database=SeuBanco;Username=postgres;Password=SuaSenha"
   
3. Execute o projeto localmente:
   ```bash
    dotnet run
    Acesse no navegador: http://localhost:5177

---

## Author
Andre Alves Pereira
- 📧 andrealves10a@gmail.com
- 🌐 [Portfólio](https://portfolio-andallves-projects.vercel.app/)
- 💼 [LinkedIn](linkedin.com/in/andre-alves-pereira/)
- 💻 [GitHub](https://github.com/andallves)
