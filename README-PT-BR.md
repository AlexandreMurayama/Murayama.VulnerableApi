# Murayama Vulnerable API

[English](README.md) | [**Português (Brasil)**](README-PT-BR.md)

![.NET](https://img.shields.io/badge/.NET-ASP.NET_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-Linguagem-512BD4?style=flat-square&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Banco_de_Dados-4169E1?style=flat-square&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Containerizado-2496ED?style=flat-square&logo=docker&logoColor=white)
![OWASP](https://img.shields.io/badge/OWASP-API_Security_Top_10-000000?style=flat-square&logo=owasp&logoColor=white)
![License](https://img.shields.io/badge/Licen%C3%A7a-MIT-yellow?style=flat-square)
[![CI - Build and Test](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/ci.yml/badge.svg)](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/ci.yml)
[![CodeQL Security Analysis](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/codeql.yml/badge.svg)](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/codeql.yml)

Uma API Web ASP.NET Core intencionalmente vulnerável, desenvolvida para o
estudo prático do **OWASP API Security Top 10 --- 2023**.

O projeto contém implementações vulneráveis e seguras de falhas comuns de
segurança em APIs, permitindo reproduzir cada vulnerabilidade, analisá-la e
compará-la com sua respectiva mitigação.

> ⚠️ **Projeto exclusivamente educacional.**
>
> Esta aplicação contém vulnerabilidades de segurança intencionais. Não a
> exponha à Internet nem a implante em um ambiente de produção.

------------------------------------------------------------------------

## 🎯 Objetivos do Projeto

A Murayama Vulnerable API foi criada como um laboratório prático de Application
Security (AppSec) com os seguintes objetivos:

-   Estudar o OWASP API Security Top 10 por meio de exemplos práticos.
-   Entender como vulnerabilidades de API aparecem no código da aplicação.
-   Reproduzir comportamentos vulneráveis através de requisições HTTP.
-   Implementar alternativas seguras para cada endpoint vulnerável.
-   Comparar implementações vulneráveis e seguras.
-   Documentar evidências de exploração e mitigações.
-   Praticar desenvolvimento seguro utilizando ASP.NET Core.
-   Construir um projeto prático de portfólio em API Security / AppSec.
-   Praticar testes automatizados de segurança com testes de integração.
-   Implementar um pipeline prático de DevSecOps com CI, SAST e controles de segurança de dependências.

------------------------------------------------------------------------

## 🛡️ OWASP API Security Top 10 --- 2023

Este projeto implementa laboratórios que cobrem as dez categorias do
OWASP API Security Top 10 2023.

  ---------------------------------------------------------------------------------------------------------------------------------------------
  \#                Vulnerabilidade    Laboratório       Documentação
  ----------------- ------------------ ----------------- --------------------------------------------------------------------------------------
  API1              Broken Object      Vulnerável vs     [Ver](docs/vulnerabilities/API1-BOLA.md)
                    Level              com validação de  
                    Authorization      propriedade       

  API2              Broken             Fraco vs protegido[Ver](docs/vulnerabilities/API2-Broken-Authentication.md)
                    Authentication     autenticação      
                                       flow              

  API3              Broken Object      Mass assignment / [Ver](docs/vulnerabilities/API3-BOPLA.md)
                    Property Level     propriedade       
                    Authorization      autorização       

  API4              Unrestricted       Sem limite vs     [Ver](docs/vulnerabilities/API4-Unrestricted-Resource-Consumption.md)
                    Resource           controlado        
                    Consumption        consumo de recursos

  API5              Broken Function    Acesso de usuário [Ver](docs/vulnerabilities/API5-Broken-Function-Level-Authorization.md)
                    Level              a funções         
                    Authorization      administrativas   

  API6              Unrestricted       Resgate repetido  [Ver](docs/vulnerabilities/API6-Unrestricted-Access-to-Sensitive-Business-Flows.md)
                    Access to          de cupom          
                    Sensitive Business                   
                    Flows                                

  API7              Server-Side        Controladas pelo  [Ver](docs/vulnerabilities/API7-Server-Side-Request-Forgery.md)
                    Request Forgery    usuário / server-side
                                       requisições       

  API8              Security           Erro interno /    [Ver](docs/vulnerabilities/API8-Security-Misconfiguration.md)
                    Misconfiguration   informações       
                                       expostas          

  API9              Improper Inventory Versão legada     [Ver](docs/vulnerabilities/API9-Improper-Inventory-Management.md)
                    Management         esquecida         

  API10             Unsafe Consumption Sem validação de  [Ver](docs/vulnerabilities/API10-Unsafe-Consumption-of-APIs.md)
                    of APIs            API de terceiros  
                                       respostas         
  ---------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------

## 🏗️ Arquitetura

O laboratório utiliza uma arquitetura deliberadamente simples para que o
comportamento de segurança permaneça fácil de entender.

``` text
                    Cliente HTTP
                        │
                        ▼
               ASP.NET Core Web API
                        │
          ┌─────────────┴─────────────┐
          │                           │
          ▼                           ▼
 Endpoints Vulneráveis          Endpoints Seguros
          │                           │
          └─────────────┬─────────────┘
                        │
                        ▼
                 Entity Framework
                        │
                        ▼
                    PostgreSQL
```

Alguns laboratórios também contêm serviços internos ou de terceiros simulados:

``` text
API7 SSRF
API ─────► recurso interno simulado

API10 Unsafe Consumption
API ─────► API de terceiro simulada
```

Todas as simulações permanecem dentro do ambiente local do laboratório.

------------------------------------------------------------------------

## 🧰 Stack Tecnológica

-   **C#**
-   **ASP.NET Core Web API**
-   **.NET**
-   **Entity Framework Core**
-   **PostgreSQL**
-   **Docker**
-   **Autenticação JWT Bearer**
-   **Autorização Baseada em Papéis**
-   **HttpClient / IHttpClientFactory**
-   **EF Core Migrations**
-   **Arquivos de requisição HTTP (`.http`)**
-   **xUnit**
-   **ASP.NET Core WebApplicationFactory**
-   **GitHub Actions**
-   **GitHub CodeQL**

------------------------------------------------------------------------

## 👥 Credenciais do Laboratório

A aplicação cria automaticamente três usuários fictícios para fins de
laboratório.

  Usuário  E-mail                   Senha         Perfil
  ------- ------------------------ ------------- -------
  Alice   `alice@murayama.local`   `Alice123!`   User
  Bob     `bob@murayama.local`     `Bob123!`     User
  Admin   `admin@murayama.local`   `Admin123!`   Admin

> ⚠️ Estas credenciais são intencionalmente públicas e existem exclusivamente
> para o laboratório local de segurança. Nunca devem ser reutilizadas em contas
> reais ou ambientes de produção.

As diferentes contas são utilizadas para demonstrar vulnerabilidades de
autenticação e autorização, como:

-   Broken Object Level Authorization (BOLA)
-   Broken Authentication
-   Broken Object Property Level Authorization (BOPLA)
-   Broken Function Level Authorization (BFLA)
-   Controle de acesso baseado em papéis

Por exemplo, autentique-se como Alice:

``` http
POST /api/auth/login
Content-Type: application/json

{
  "email": "alice@murayama.local",
  "password": "Alice123!"
}
```

Depois utilize o JWT retornado:

``` http
Authorization: Bearer <ACCESS_TOKEN>
```

Nunca faça commit de tokens JWT gerados no repositório.

------------------------------------------------------------------------

## 🔐 Autenticação

A API utiliza autenticação JWT Bearer.

Após autenticar com uma das contas do laboratório, a API retorna um access token.

Requisições autenticadas devem fornecer o token utilizando:

``` http
Authorization: Bearer <ACCESS_TOKEN>
```

As chaves de assinatura JWT são fornecidas por configuração local e não são
commitadas no repositório.

> Nunca faça commit de tokens de autenticação gerados ou chaves de assinatura no
> repositório.

------------------------------------------------------------------------

## 🧪 Estrutura dos Laboratórios

A maioria das vulnerabilidades segue a mesma estrutura:

``` text
Implementação vulnerável
          │
          ▼
Reproduzir a vulnerabilidade
          │
          ▼
Capturar evidências
          │
          ▼
Implementação segura
          │
          ▼
Verificar a mitigação
```

Os endpoints geralmente são separados utilizando:

``` text
/api/vulnerable/...
```

e:

``` text
/api/secure/...
```

Isso permite comparar lado a lado as implementações inseguras e seguras.

------------------------------------------------------------------------

## 🔎 Exemplo --- Broken Object Level Authorization

Um endpoint vulnerável pode recuperar um pedido diretamente a partir de um
identificador controlado pelo cliente:

``` text
GET /api/vulnerable/orders/2
```

sem verificar a propriedade do objeto.

Conceitualmente:

``` text
Alice
  │
  │ solicita o Pedido 2
  ▼
API
  │
  ▼
O pedido existe?
  │
  └── Sim → Retorna o pedido ❌
```

A implementação segura também verifica se o usuário autenticado é o proprietário
do objeto solicitado:

``` text
Alice
  │
  │ solicita o Pedido 2
  ▼
API
  │
  ▼
O pedido pertence à Alice?
  │
  ├── Sim → Retorna
  └── Não → Rejeita ✅
```

Veja a documentação completa do laboratório em:

[API1 --- Broken Object Level
Authorization](docs/vulnerabilities/API1-BOLA.md)

------------------------------------------------------------------------

## 📂 Estrutura do Projeto

``` text
.
├── .github/
│   └── workflows/
│       ├── ci.yml
│       ├── codeql.yml
│       └── dependency-review.yml
│
├── Murayama.VulnerableApi/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Services/
│   ├── Settings/
│   ├── Murayama.VulnerableApi.csproj
│   └── Murayama.VulnerableApi.http
│
├── Murayama.VulnerableApi.Tests/
│   ├── Infrastructure/
│   │   ├── CustomWebApplicationFactory.cs
│   │   ├── RecordingHttpClientFactory.cs
│   │   ├── RecordingHttpMessageHandler.cs
│   │   └── SsrfWebApplicationFactory.cs
│   ├── Integration/
│   │   └── HealthCheckTests.cs
│   ├── Security/
│   │   ├── BolaTests.cs
│   │   ├── BoplaTests.cs
│   │   ├── BrokenAuthenticationTests.cs
│   │   └── SsrfTests.cs
│   ├── AssemblyInfo.cs
│   └── Murayama.VulnerableApi.Tests.csproj
│
├── docs/
│   ├── images/
│   │   ├── api1-bola/
│   │   ├── api2-broken-authentication/
│   │   ├── api3-bopla/
│   │   ├── api4-unrestricted-resource-consumption/
│   │   ├── api5-bfla/
│   │   ├── api6-sensitive-business-flows/
│   │   ├── api7-ssrf/
│   │   ├── api8-security-misconfiguration/
│   │   ├── api9-improper-inventory-management/
│   │   └── api10-unsafe-consumption-of-apis/
│   │
│   └── vulnerabilities/
│       ├── API1-BOLA.md
│       ├── API2-Broken-Authentication.md
│       ├── API3-BOPLA.md
│       ├── API4-Unrestricted-Resource-Consumption.md
│       ├── API5-Broken-Function-Level-Authorization.md
│       ├── API6-Unrestricted-Access-to-Sensitive-Business-Flows.md
│       ├── API7-Server-Side-Request-Forgery.md
│       ├── API8-Security-Misconfiguration.md
│       ├── API9-Improper-Inventory-Management.md
│       └── API10-Unsafe-Consumption-of-APIs.md
│
├── .env.example
├── compose.yaml
├── LICENSE
└── README.md
```

------------------------------------------------------------------------

## 🚀 Executando o Laboratório

### Requisitos

Instale:

-   .NET SDK
-   Docker
-   Git

Verifique o .NET:

``` bash
dotnet --version
```

Verifique o Docker:

``` bash
docker --version
```

### Configuração do Ambiente

Crie um arquivo `.env` local com base no `.env.example`.

Exemplo:

``` dotenv
POSTGRES_DB=murayama_vulnerable_api
POSTGRES_USER=your_postgres_user
POSTGRES_PASSWORD=your_postgres_password

JWT_KEY=replace_with_a_development_jwt_signing_key
```

O arquivo `.env` é ignorado pelo Git e não deve ser commitado.

### Executando com Docker Compose

``` bash
docker compose up --build -d
```

Verifique os containers:

``` bash
docker ps
```

A configuração do Docker Compose vincula os serviços do laboratório à máquina
local.

A API estará disponível em:

``` text
http://localhost:8080
```

### Health Check

``` http
GET http://localhost:8080/api/health
```

Resposta esperada:

``` http
HTTP/1.1 200 OK
```

### Execução Local com .NET

Ao executar diretamente pelo ambiente de desenvolvimento .NET, aplique as
migrations do banco a partir do diretório que contém
`Murayama.VulnerableApi.csproj`:

``` bash
dotnet ef database update
```

Depois:

``` bash
dotnet run
```

A instância local de desenvolvimento utilizada ao longo do laboratório normalmente
estará disponível em:

``` text
http://localhost:5248
```

A porta exata pode variar conforme a configuração local de desenvolvimento.

------------------------------------------------------------------------

## 🧪 Testes HTTP

O repositório contém:

``` text
Murayama.VulnerableApi.http
```

Este arquivo contém requisições utilizadas para reproduzir os laboratórios.

Os exemplos incluem:

``` http
GET {{host}}/api/vulnerable/orders/2
Authorization: Bearer {{token}}
```

e suas versões seguras correspondentes:

``` http
GET {{host}}/api/secure/orders/2
Authorization: Bearer {{token}}
```

As requisições podem ser executadas em IDEs com suporte a arquivos `.http`.

------------------------------------------------------------------------

## ⚙️ DevSecOps & Testes Automatizados de Segurança

O projeto inclui um pipeline DevSecOps projetado para validar automaticamente a
aplicação, suas dependências e comportamentos de segurança selecionados.

### Integração Contínua

O GitHub Actions executa automaticamente o pipeline de CI em pushes e pull
requests direcionados à branch `main`.

O pipeline executa:

``` text
Pull Request / Push
        │
        ▼
Restaurar Dependências
        │
        ▼
Build (.NET 10)
        │
        ▼
Ambiente de Testes PostgreSQL 17
        │
        ▼
Testes Automatizados
        │
        ├── Testes de Integração
        └── Testes de Segurança
```

Um container de serviço PostgreSQL 17 dedicado é criado pelo GitHub Actions para
o ambiente de testes automatizados. As migrations e os dados de seed são aplicados
a esse banco isolado antes da execução dos testes.

### Testes Automatizados de Segurança

Cenários selecionados do OWASP API Security são reproduzidos através de testes
automatizados de integração utilizando **xUnit** e **WebApplicationFactory**.

| OWASP | Teste de Segurança | Comportamento Validado |
|---|---|---|
| API1:2023 | BOLA | Um usuário autenticado consegue acessar um objeto pertencente a outro usuário |
| API2:2023 | Broken Authentication | Tentativas repetidas de autenticação inválida são aceitas sem rate limiting |
| API3:2023 | BOPLA | Um usuário comum consegue modificar a propriedade sensível `Role` |
| API7:2023 | SSRF | Uma URL de loopback controlada pelo usuário alcança o cliente HTTP server-side |

> **Importante:** Esta é uma aplicação intencionalmente vulnerável.
>
> Um teste de segurança aprovado **não** significa que o endpoint vulnerável está
> seguro. Esses testes funcionam como especificações executáveis do laboratório
> educacional: eles verificam que o comportamento vulnerável esperado ainda pode
> ser reproduzido.
>
> As implementações seguras correspondentes permanecem documentadas separadamente
> e demonstram as respectivas mitigações.

O teste de SSRF utiliza um `HttpMessageHandler` controlado para verificar o destino
solicitado pela aplicação sem realizar uma conexão real com um serviço interno ou
de loopback.

### Static Application Security Testing (SAST)

O repositório utiliza **GitHub CodeQL** para realizar análise estática de segurança
da base de código C#.

A análise do CodeQL é executada automaticamente pelo GitHub Actions e participa do
fluxo de segurança do repositório.

``` text
Código-Fonte
    │
    ▼
Análise CodeQL
    │
    ▼
Findings de Segurança
    │
    ▼
GitHub Code Scanning
```

### Segurança de Dependências

O **Dependency Review** é executado em pull requests para identificar alterações de
dependências potencialmente vulneráveis ou arriscadas antes do merge.

O **Dependabot** está habilitado para monitorar as dependências do projeto e
apresentar atualizações de segurança e de dependências disponíveis.

### Proteção de Segredos

O repositório utiliza controles de segurança do GitHub para reduzir o risco de
credenciais serem commitadas acidentalmente:

-   Secret Scanning
-   Push Protection

Senhas reais de banco de dados, chaves de assinatura JWT e access tokens gerados
não são armazenados no repositório.

### Proteção de Branch e Security Gates

A branch `main` é protegida por verificações obrigatórias em pull requests.

As alterações são validadas por CI automatizado e análise de segurança antes do
merge.

O repositório utiliza atualmente os seguintes workflows do GitHub Actions:

``` text
.github/workflows/
├── ci.yml
├── codeql.yml
└── dependency-review.yml
```

Em conjunto, esses controles demonstram um ciclo básico de entrega segura de
software combinando validação automatizada de build, testes de integração, testes
de segurança, SAST, análise de dependências e proteção de segredos.

------------------------------------------------------------------------


## 🔐 Security Assessment / Pentest

Após a implementação dos laboratórios da API e dos controles automatizados de
segurança, a aplicação foi submetida a um teste de intrusão autorizado a partir
de um ambiente Kali Linux.

A avaliação começou com uma abordagem **black-box**, utilizando descoberta de
serviços, reconhecimento HTTP e enumeração de endpoints. Após a identificação
local da especificação da API, a avaliação prosseguiu como **gray-box**, permitindo
uma cobertura sistemática da superfície de ataque exposta.

O pentest incluiu:

-   Descoberta de serviços e endpoints com Nmap, FFUF e curl
-   Análise de autenticação e JWT
-   Broken Object Level Authorization (BOLA / IDOR)
-   Broken Object Property Level Authorization (BOPLA)
-   Broken Function Level Authorization (BFLA)
-   Server-Side Request Forgery (SSRF)
-   Exposição de propriedades sensíveis
-   Security misconfiguration e tratamento excessivamente detalhado de erros
-   Paginação excessiva e controles de consumo de recursos
-   Unsafe Consumption of Third-Party API Data
-   Rate limiting de autenticação e controles contra abuso automatizado

### Resultados do Pentest

A avaliação confirmou **10 findings de segurança**.

| ID | Finding | CVSS v3.1 proposto |
|---|---|---:|
| F-01 | Server-Side Request Forgery (SSRF) | 8.1 High |
| F-02 | Broken Object Level Authorization (BOLA / IDOR) | 6.5 Medium |
| F-03 | BOPLA / Mass Assignment levando a Privilege Escalation | 8.8 High |
| F-04 | Sensitive Property Exposure | 4.3 Medium |
| F-05 | Verbose Error Handling / Sensitive Information Disclosure | 6.5 Medium |
| F-06 | Broken Function Level Authorization (BFLA) | 6.5 Medium |
| F-07 | Excessive Pagination / Unrestricted Resource Consumption | 4.3 Medium |
| F-08 | BOLA em Orders Search | 6.5 Medium |
| F-09 | Unsafe Consumption of Third-Party API Data | 5.3 Medium |
| F-10 | Missing Authentication Rate Limiting | 5.3 Medium |

O finding de maior prioridade demonstrou um fluxo completo de
**Privilege Escalation de User → Admin**. Um usuário comum conseguiu modificar a
propriedade sensível `Role`, autenticar novamente, obter privilégios
administrativos efetivos e acessar um endpoint administrativo.

A avaliação também identificou uma condição de BOLA em um endpoint destinado a
representar uma implementação segura, demonstrando que a autorização precisa ser
validada de forma consistente em todas as operações que expõem o mesmo recurso
protegido.

### Testes Negativos e Controles que Funcionaram

O pentest também registrou controles que funcionaram corretamente:

-   Adulteração da role do JWT sem uma assinatura válida foi rejeitada.
-   JWTs inválidos ou sem assinatura válida foram rejeitados.
-   Um usuário comum teve o acesso a `/api/v2/users` negado.
-   O endpoint seguro de pedido individual impediu acesso entre usuários.
-   O endpoint seguro de SSRF bloqueou destinos locais e privados.
-   O endpoint seguro de autenticação aplicou rate limiting.
-   A persistência duplicada de cupons foi impedida pela constraint de unicidade do banco.

Esses testes negativos ajudam a diferenciar vulnerabilidades confirmadas de
vetores que foram testados, mas corretamente bloqueados.

### Documentação do Pentest

A documentação pública do pentest é mantida separadamente do README principal:

-   [Pentest Assessment](docs/pentest/PENTEST.pt-BR.md)

A documentação pública é sanitizada e não contém JWTs gerados, hashes de senha,
caminhos pessoais do sistema de arquivos ou outras evidências sensíveis
desnecessárias.

------------------------------------------------------------------------

## 🗄️ Banco de Dados

O PostgreSQL é utilizado para persistir os dados do laboratório.

O Entity Framework Core é responsável por:

-   mapeamento de entidades
-   acesso ao banco de dados
-   migrations de schema
-   relacionamentos
-   constraints de banco de dados

Alguns controles de segurança também são aplicados intencionalmente na camada de
banco de dados.

Por exemplo, o laboratório API6 utiliza uma constraint única para impedir múltiplos
resgates do mesmo cupom pelo mesmo usuário:

``` text
UNIQUE(UserId, CouponCode)
```

Isso demonstra defesa em profundidade entre a validação da aplicação e a
integridade do banco de dados.

------------------------------------------------------------------------

## 🔒 Técnicas de Segurança Demonstradas

As implementações seguras demonstram técnicas que incluem:

-   Autorização em nível de objeto
-   Autorização em nível de propriedade
-   Controle de acesso baseado em papéis
-   Autenticação JWT
-   Modelos de entrada baseados em DTOs
-   Validação server-side de regras de negócio
-   Constraints de unicidade no banco
-   Limites de recursos
-   Validação de destinos contra SSRF
-   Bloqueio de endereços privados e loopback
-   Tratamento seguro de erros
-   Logging de exceções no servidor
-   Gerenciamento do ciclo de vida da API
-   Validação de respostas de APIs externas
-   Timeouts HTTP
-   Tratamento de falhas upstream

------------------------------------------------------------------------

## 📸 Evidências

Cada laboratório contém screenshots demonstrando os comportamentos vulnerável e
seguro.

As evidências são armazenadas em:

``` text
docs/images/
```

Os exemplos incluem:

-   respostas HTTP vulneráveis
-   respostas HTTP seguras
-   falhas de autorização
-   verificações no PostgreSQL
-   logs do servidor
-   serviços internos simulados
-   respostas simuladas de APIs de terceiros

Os relatórios detalhados fazem referência direta a essas imagens.

------------------------------------------------------------------------

## ⚠️ Aviso de Segurança

Este repositório contém código intencionalmente vulnerável.

Ele foi desenvolvido para:

-   educação em cybersecurity
-   estudo de Application Security
-   prática de secure coding
-   testes de segurança de APIs
-   exercícios em laboratório local

Ele **não** foi desenvolvido para:

-   implantação em produção
-   hospedagem exposta à Internet
-   processamento de dados reais de usuários
-   armazenamento de credenciais reais
-   manipulação de informações sensíveis

Execute a aplicação apenas em um ambiente isolado e controlado.

------------------------------------------------------------------------

## 📚 Referências

-   [OWASP API Security
    Project](https://owasp.org/www-project-api-security/)
-   [OWASP API Security Top 10 ---
    2023](https://owasp.org/API-Security/editions/2023/en/0x11-t10/)
-   [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
-   [Documentação do Microsoft ASP.NET Core](https://learn.microsoft.com/aspnet/core/)
-   [Documentação do Entity Framework Core](https://learn.microsoft.com/ef/core/)

------------------------------------------------------------------------

## 👤 Autor

**Alexandre Murayama**

Desenvolvedor de Software em transição para Cybersecurity, com foco em:

-   Application Security
-   API Security
-   Penetration Testing
-   Secure Software Development

------------------------------------------------------------------------

## 📄 Licença

Este projeto está licenciado sob a **MIT License**.

Consulte [LICENSE](LICENSE) para mais detalhes.