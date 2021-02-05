# ClientsManager
A solução para o gerenciador de clientes é dividia em 3 projetos:
- Uma WebApi em asp.net core (ClientsManager.WebAPI).
- Um projeto para teste unitário dessa WebAPI (ClientsManager.WebAPI.Tests.MSTests).
- Um projeto de aplicação (em UWP) atuando como client dessa API (ClientesManager (Universal Windows)).

Como banco de dados foi utilizado o sqlite para facilitar o consumo dos dados. O banco é um arquivo chamado "Clientes.db" que está no corpo do projeto.
Foi utilizado o EntityFrameWorkCore para realização das Migrations e criação do banco. As Migrations já foram realizadas.

Para rodar o programa cliente (Em UWP) é necessário o Visual Studio.
O Windows 10 precisa estar em "modo desenvolvedor" para rodar o projeto em UWP. O app permite que sejam adicionados mais clientes bem como updates deles,
além de criação de novos endereços.

As funcionalidades completas da API podem ser inpecionadas pela página do swagger.
