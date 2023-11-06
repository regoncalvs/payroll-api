# payroll-api
Esta API é responsável por gerenciar as informações de pagamento dos funcionários de uma empresa. Ela oferece recursos para criar e buscar um funcionário, bem como gerar extratos de contracheque com todos os lançamentos em folha salarial.

# Funcionalidades
Criação e busca de funcionário.
Geração de contracheques com detalhes sobre as remunerações e descontos.

# Tecnologias Utilizadas
ASP.NET Core para o desenvolvimento da API.
Entity Framework Core para a camada de persistência de dados.
MediatR para o design de padrões de CQRS e manipulação de comandos e consultas, com uso de Eventos de Domínio.
AutoMapper para mapear entidades de domínio para modelos de leitura.
FluentValidation para validação de dados de entrada.
XUnit e Moq para testes unitários.

# Pré-requisitos
ASP.NET Core.
Entity Framework Core.
SQL Server.
Docker.

# Instalação
Clone este repositório para o seu ambiente local.
Certifique-se de ter os pré-requisitos instalados e configurados.
Na pasta raiz da solução, execute o comando docker-compose up --build para criar e iniciar os contêineres.

# Como Usar
A documentação detalhada sobre como usar a API e suas funcionalidades está disponível no endpoint de documentação. Certifique-se de ler a documentação para obter informações sobre os endpoints disponíveis.
