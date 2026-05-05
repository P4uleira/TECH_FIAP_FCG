# 🎮 FIAP Cloud Games (FCG)
---

## 📋 Índice

- [Objetivos](#-objetivos)
- [Funcionalidades](#-funcionalidades)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração](#-configuração)
- [Como Executar](#-como-executar)
- [Endpoints](#-endpoints)
- [Autenticação](#-autenticação)
- [Testes](#-testes)
- [DDD e Event Storming](#-ddd-e-event-storming)

---

## 🎯 Objetivos

O FIAP Cloud Games é um MVP de plataforma de jogos digitais com os seguintes objetivos:

- Permitir o cadastro e autenticação de usuários com dois níveis de acesso
- Gerenciar um catálogo de jogos com suporte a promoções
- Disponibilizar uma biblioteca pessoal de jogos para cada usuário
- Aplicar boas práticas de arquitetura, DDD, testes e segurança

---

## ✅ Funcionalidades

### Usuários
- Cadastro com nome, e-mail e senha
- Validação de formato de e-mail (`xxx@xxx.xxx`)
- Validação de senha forte (mínimo 8 caracteres, letras, números e caractere especial)
- Dois níveis de acesso:
  - **Usuário** — acessa a plataforma e gerencia sua biblioteca de jogos
  - **Administrador** — cadastra jogos, administra usuários e cria promoções

### Jogos
- CRUD completo de jogos (restrito ao Administrador)
- Validação de título único, descrição obrigatória e preço não negativo
- Suporte a jogos gratuitos (preço zero)

### Promoções
- Aplicação de preço promocional por jogo
- Data de expiração opcional
- Preço promocional exibido automaticamente enquanto a promoção estiver ativa

### Biblioteca
- Usuário pode adicionar jogos à sua biblioteca
- Exibe data de aquisição de cada jogo
- Impede adição duplicada do mesmo jogo

### Autenticação
- Login via e-mail e senha
- Retorno de token JWT com expiração configurável
- Autorização por roles (`Usuário` / `Administrador`)

---

## 🏛 Arquitetura

O projeto segue uma arquitetura em camadas baseada nos princípios de **Domain-Driven Design (DDD)**, organizada como monolito para facilitar o desenvolvimento ágil do MVP.

```
Endpoint (HTTP)
     ↓
API (Controllers + Middleware)
     ↓
Application (Handlers — orquestração)
     ↓
Domain (Entities + Services — regras de negócio)
     ↓
Infra (Repositories + EF Core — persistência)
```

### Camadas

| Camada | Responsabilidade |
|---|---|
| **API** | Recebe requisições HTTP, valida entrada, retorna respostas |
| **Application** | Orquestra o fluxo entre Domain e Infra |
| **Domain** | Entidades, interfaces e regras de negócio puras |
| **Infra** | Implementação dos repositórios e acesso ao banco |

---

## 🛠 Tecnologias

| Tecnologia | Versão | Uso |
|---|---|---|
| .NET | 8.0 | Framework principal |
| ASP.NET Core | 8.0 | API REST com Controllers MVC |
| Entity Framework Core | 8.0 | ORM e migrations |
| SQL Server | — | Banco de dados relacional |
| AutoMapper | 12.0.1 | Mapeamento entre entidades e DTOs |
| BCrypt.Net | — | Hash de senhas |
| JWT Bearer | 8.0 | Autenticação e autorização |
| Swagger / Swashbuckle | 6.6.2 | Documentação da API |
| xUnit | 2.9.0 | Framework de testes |
| Moq | 4.20.70 | Mock de dependências |
| FluentAssertions | 6.12.0 | Asserções nos testes |

---

## 📁 Estrutura do Projeto

```
FIAP_CLOUD_GAMES/
├── FCG.Api/                        # Camada de apresentação
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── JogosController.cs
│   │   ├── UsuarioController.cs
│   │   └── BibliotecaController.cs
│   ├── Middlewares/
│   │   └── TratamentoErroMiddleware.cs
│   └── Program.cs
│
├── FCG.Application/                # Camada de aplicação
│   ├── Handlers/
│   │   ├── AuthHandler.cs
│   │   ├── JogoHandler.cs
│   │   ├── UsuarioHandler.cs
│   │   └── BibliotecaHandler.cs
│   └── Interfaces/
│       ├── IAuthHandler.cs
│       ├── IJogoHandler.cs
│       ├── IUsuarioHandler.cs
│       └── IBibliotecaHandler.cs
│
├── FCG.Domain/                     # Camada de domínio
│   ├── Entity/
│   │   ├── Jogo.cs
│   │   ├── Usuario.cs
│   │   ├── Acesso.cs
│   │   └── Biblioteca.cs
│   ├── DTO/
│   │   ├── Requests/
│   │   └── Responses/
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   └── Services/
│   ├── Services/
│   │   ├── JogoService.cs
│   │   ├── UsuarioService.cs
│   │   └── AuthService.cs
│   ├── Mappings/
│   │   ├── JogoProfile.cs
│   │   └── BibliotecaProfile.cs
│   └── Exceptions/
│       └── DomainException.cs
│
├── FCG.Infra/                      # Camada de infraestrutura
│   ├── Data/
│   │   ├── FCGDbContext.cs
│   │   └── Configurations/
│   │       ├── JogoConfiguration.cs
│   │       ├── UsuarioConfiguration.cs
│   │       ├── AcessoConfiguration.cs
│   │       └── BibliotecaConfiguration.cs
│   ├── Repositories/
│   │   ├── JogoRepository.cs
│   │   ├── UsuarioRepository.cs
│   │   └── BibliotecaRepository.cs
│   └── Migrations/
│
└── FCG.Tests/                      # Testes
    ├── UsuarioServiceTests.cs      # Testes unitários
    └── JogoServiceTddTests.cs  # Testes com metodologia TDD
```

---

## ⚙ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (ou SQL Server Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

## 🔧 Configuração

### 1. Clone o repositório

```bash
git clone https://github.com/P4uleira/TECH_FIAP_FCG.git
cd TECH_FIAP_FCG/FIAP_CLOUD_GAMES
```

### 2. Configure a connection string

Edite o arquivo `FCG.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=FCG;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA_MINIMO_32_CARACTERES",
    "Issuer": "FCG.Api",
    "Audience": "FCG.Client",
    "ExpiracaoHoras": "8"
  }
}
```

### 3. Execute as migrations

```bash
Update-Database -StartupProject FCG.Api -Project FCG.Infra   --> Package Manager Console
```

Isso criará o banco de dados com as tabelas e os dados iniciais de `Acesso` (Usuário e Administrador).

---

## ▶ Como Executar

```bash
cd FCG.Api
dotnet run
```

A API estará disponível em:
- `https://localhost:7000` (HTTPS)
- `http://localhost:5000` (HTTP)

A documentação Swagger estará em:
- `https://localhost:7000/swagger`

---

## 📡 Endpoints

### Auth
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/auth/login` | Público | Autentica e retorna token JWT |

### Usuários
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| POST | `/api/usuario` | Público | Cadastra novo usuário |
| GET | `/api/usuario` | Administrador | Lista todos os usuários |
| GET | `/api/usuario/{id}` | Administrador | Busca usuário por ID |
| PUT | `/api/usuario` | Autenticado | Atualiza dados do usuário |
| DELETE | `/api/usuario/{id}` | Administrador | Remove usuário |

### Jogos
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| GET | `/api/jogos` | Público | Lista todos os jogos |
| GET | `/api/jogos/{id}` | Público | Busca jogo por ID |
| POST | `/api/jogos` | Administrador | Cadastra novo jogo |
| PUT | `/api/jogos` | Administrador | Atualiza jogo |
| DELETE | `/api/jogos/{id}` | Administrador | Remove jogo |
| PUT | `/api/jogos/{id}/promocao` | Administrador | Aplica promoção ao jogo |
| DELETE | `/api/jogos/{id}/promocao` | Administrador | Remove promoção do jogo |

### Biblioteca
| Método | Rota | Acesso | Descrição |
|---|---|---|---|
| GET | `/api/biblioteca` | Autenticado | Lista jogos da biblioteca do usuário |
| POST | `/api/biblioteca/{jogoId}` | Autenticado | Adiciona jogo à biblioteca |
| DELETE | `/api/biblioteca/{jogoId}` | Administrador | Remove jogo da biblioteca |

---

## 🔐 Autenticação

A API utiliza autenticação via **JWT Bearer Token**.

### 1. Criação de um usuário incial.

```http
POST /api/usuario/
Content-Type: application/json

Atualmente o sistema possui dois tipos de acesso (Usuário e ADM)

Guid fixado para ambos:

8D87CAAF-6345-4865-9057-45A8B6B5D882	Administrador
0C6CD9E6-F3EA-44A0-9C4D-ECD7906089DE	Usuário

Ao realizar o cadastro siga os próximos passos.

```

### 2. Faça login

```http
POST /api/auth/login
Content-Type: application/json

Ex.:
{
  "email": "admin@fcg.com",
  "senha": "Admin@123"
}
```

### 3. Use o token retornado

```http
GET /api/jogos
Authorization: Bearer {seu_token_aqui}
```

### 4. No Swagger

Clique no botão **Authorize** (🔒) no canto superior direito, cole o token no formato `Bearer {token}` e confirme.

### Níveis de acesso

| Role | Permissões |
|---|---|
| `Usuário` | Acessa a plataforma, gerencia sua biblioteca |
| `Administrador` | Tudo do Usuário + cadastra jogos, administra usuários e cria promoções |

---

## 🧪 Testes

O projeto possui dois conjuntos de testes:

### Testes Unitários — `UsuarioServiceTests`

Validam as regras de negócio do `UsuarioService`:
- Validação de nome (obrigatório, máximo 100 caracteres)
- Validação de e-mail (formato `xxx@xxx.xxx`, máximo 100 caracteres)
- Validação de senha forte (mínimo 8 caracteres, letras, números e caractere especial)

### TDD — `JogoServiceTddTests`

Demonstram o ciclo **Red → Green → Refactor** aplicado ao módulo de `Jogo`:
- **RED** — teste escrito antes da implementação, falha intencionalmente
- **GREEN** — implementação mínima para o teste passar
- **REFACTOR** — melhoria do código sem quebrar os testes

Cobre 7 ciclos TDD: título, descrição, preço, data de criação, duplicidade e promoções.

### Executar todos os testes

```bash
dotnet test
```

### Executar por classe

```bash
dotnet test --filter "ClassName~UsuarioServiceTests"
dotnet test --filter "ClassName~JogoServiceTddTests"
```

### Executar um teste específico

```bash
dotnet test --filter "FullyQualifiedName~ValidarCriacao_TituloVazio"
```

---

## 📐 DDD e Event Storming

O projeto foi modelado seguindo os princípios de **Domain-Driven Design**:

### Entidades do Domínio

| Entidade | Descrição |
|---|---|
| `Usuario` | Representa o cliente da plataforma |
| `Jogo` | Produto disponível na plataforma |
| `Acesso` | Nível de permissão do usuário (Usuário / Administrador) |
| `Biblioteca` | Relação entre usuário e seus jogos adquiridos |

### Regras de Negócio (Domain Services)

| Service | Regras |
|---|---|
| `UsuarioService` | Validação de nome, e-mail e senha forte |
| `JogoService` | Validação de título único, preço e promoções |
| `AuthService` | Geração de JWT e hash de senha com BCrypt |

### Event Storming

A documentação de Event Storming com os fluxos de **Criação de Usuários** e **Criação de Jogos** está disponível no Miro:

> 🔗 [Link da documentação DDD no Miro](https://miro.com/app/board/uXjVHfdUwn8=/?share_link_id=437338695684)

---

## 👥 Equipe

| Nome | Username Discord |
|---|---|
| Paulo Ricardo P. de Oliveira | P4uleira#7489 |

---

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos no Tech Challenge da FIAP.
