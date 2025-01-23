# StreamWave
 
StreamWave é uma aplicação de streaming de vídeo, onde é utilzado Nginx para servir as mídias e ASP.NET Core para gerenciar as requisições de lógica de aplicação e controles de acesso, realizando controle de acesso em vídeos via roles com JWT. 


## Tecnologias
- C# .NET
- ASP.NET Core
- Entity Framework
- SQL Server
- Swagger
- Nginx
- JWT Bearer

## Endpoints
### Playback
- __POST /playback/video:__ Usuários autenticados podem recuperar um vídeo pelo nome. Caso o vídeo não seja permitido para sua role o acesso sera negado. 

### User
- __POST /login:__ Realiza autenticação de usuários e devolve JWT.
- __POST /user/create:__ Cria novos usuários, com diferentes roles. \
__OBS.:__ Para criar usuários com role de admin é necessário estar autenticado com um token com role admin.

### Video [Authorize]
- __GET /videos{name}__ Recupera informações de um vídeo pelo nome. 
- __GET /videos/get-all__ Recupera informações de todos os vídeos cadastrados.
- __POST /videos/upload__ Permite administradores realizarem cadastro e upload de um vídeo com extensão .mp4, podendo especificar qual role de usuário será permitida acessar.


## Tabelas
![Texto Alternativo](https://github.com/RodrigoLorensiMarques/StreamWave/blob/main/DbDiagrama.png)


## Como Rodar
### Requisitos:
- [.NET 9](https://dotnet.microsoft.com/pt-br/download)
- [Docker](https://docs.docker.com/get-started/get-docker/)

### Passo a Passo:
1. Em seu terminal, acesse a pasta raiz do repositório

2. Suba os serviços:
   ```
   docker-compose up
   ```
5. No diretório __WebApi__, restaure os pacotes:
   ```
   dotnet restore
   ```

7. Aplique as migrations:
   ```
   dotnet-ef database update
   ```
8. Crie as roles no banco:
   ```
   INSERT INTO roles
   VALUES ('administrator'), ('premium'), ('standard')
   ```
9. Crei um usuário admin no banco: \
   __User:__ admin __Password:__ admin123
   ```
   INSERT INTO users
   VALUES ('admin', '$2a$11$69ddoeK/rQFdU.HD81IKSeCOADOdDoizztgXZiFI1rvPTemw2xAjS', 1)
   ```

11. Pronto! Agora você pode acessar o [http://localhost:5124/swagger](http://localhost:5077/swagger/index.html) para ter acesso a interface do Swagger.
