# StreamWave
 
StreamWave é uma plataforma de streaming de vídeo, onde é utilzado Nginx para servir as mídias e ASP.NET Core para gerenciar as requisições de lógica de aplicação e controles de acesso, realizando controle de acesso em vídeos via roles com JWT. 


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
- __GET /videos{name}:__ Recupera informações de um vídeo pelo nome. 
- __GET /videos/get-all__ Recupera informações de todos os vídeos cadastrados.
- __POST /videos/upload__ Realiza cadastro e upload de um vídeo com extensão .mp4, podendo especificar qual role de usuário sera permitido acessar.


## Tabelas
![Texto Alternativo](https://github.com/RodrigoLorensiMarques/StreamWave/blob/main/DbDiagrama.png)


## Como Rodar
