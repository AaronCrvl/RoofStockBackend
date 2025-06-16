# 📋 Backend API - RoofTopStock

## 📋 Resumo Executivo
A API do RoofTopStock é um backend construído utilizando ASP.NET Core, seguindo uma arquitetura de camadas para gerenciar os recursos da plataforma de gestão de estoques. A aplicação suporta autenticação via JWT, log de eventos com Serilog, validação de dados com FluentValidation, e um design robusto com foco em separação de responsabilidades.

### Funcionalidades Principais
- Autenticação JWT: A API utiliza tokens JWT para autenticação segura e escalável.
- Arquitetura em Camadas: A aplicação segue uma arquitetura de camadas (Controladores, Serviços, Validadores, Repositório, Contexto).
- Validação: Uso do FluentValidation para garantir dados consistentes.
- Logs: Implementação de logging centralizado com Serilog.
- Banco de Dados no Azure: O banco de dados é hospedado no Azure SQL Database e a conexão é feita utilizando o Microsoft.Data.SqlClient.
- Acesso via API: A API permite interações seguras e eficientes com o banco de dados, como consultas e manipulação de dados de estoque.

### 🛠️ Tecnologias Utilizadas
- ASP.NET Core: Framework para desenvolvimento da API.
- JWT (JSON Web Token): Autenticação e autorização segura.
- Serilog: Logging estruturado para melhor monitoramento e depuração.
- FluentValidation: Validação de dados no backend de maneira clara e robusta.
- Entity Framework Core: ORM para interação com o banco de dados.
- Microsoft.Data.SqlClient: Biblioteca utilizada para conectar a API com o banco de dados Azure SQL Database. A escolha dessa biblioteca garante uma conexão eficiente e segura.
- Azure SQL Database: O banco de dados está hospedado no Azure, proporcionando escalabilidade, alta disponibilidade e segurança.

### 📂 Estrutura do Projeto
O projeto segue uma arquitetura em camadas, onde cada camada possui uma responsabilidade específica. As camadas incluem:
- Controladores (Controladores): Responsáveis por gerenciar as requisições HTTP, chamar os serviços e retornar respostas ao cliente.
- Serviços (Serviços): Contêm a lógica de negócio da aplicação, realizando operações mais complexas que os controladores não devem gerenciar.
- Validadores (Validadores): Implementação do FluentValidation para validar as entradas antes de serem processadas pela lógica de negócios
- Repositório (Repositório): Realiza operações de leitura e escrita no banco de dados, abstraindo a camada de persistência.
- Contexto (Contexto): Gerencia a interação com o banco de dados utilizando o Entity Framework Core.

###  ⚙️ Setup e Instalação
Pré-requisitos
- .NET SDK versão 6.0 ou superior.
- Azure SQL Database: Um banco de dados configurado no Azure para armazenar as informações da aplicação.

Made with the help of 🎶 and my 🐶. / Feito com a ajuda de muita 🎶 e meu 🐶.