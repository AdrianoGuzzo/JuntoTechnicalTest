# Teste Backend – Junto Seguros
## A tarefa é criar um microserviço de usuários em .Net.
## Critérios de aceitação:

- Versionar no github
- 80% de cobertura de testes unitários
- Container
- Arquitetura
- Logs e Observabilidade
- API Rest
- CRUD de usuários, geração de token seguro e troca de password (não precisa enviar o e-mail)
- Uso de qualquer banco de dados relacional;

## Arquitetura do projeto:
Essa arquitetura foi desenvolvida criando um serviço de autenticação com Identity Server, utilizando o framework Duende. Foi criada também uma API que consome esse serviço de autenticação para validar tokens JWT e para criar e alterar senhas de usuários.

![Logo do Markdown](https://macoratti.net/22/06/duende_protapiusercred11.jpg)
