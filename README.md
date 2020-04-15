# Web Service

> Aplicação feita para servi um aplicativo feito em Xamarin para organizações de jogos (peladas) de futebol, futsal, volei, etc.

### Recursos desenvolvidos

#### Home
 - [X] Autenticação:  ```GET: api/home/login```
#### User
 - [X] Listar usuários:  ```GET: api/user/```
 - [X] Mostrar usuário:  ```GET: api/user/{id}```
 - [X] Registrar usuário: ```POST: api/user/```
 - [X] Atualizar usuário:  ```PUT: api/user/{id}```
 - [X] Excluir usuário:  ```DELETE: api/user/{id}```
#### Athlete
 - [X] Listar atletas:  ```GET: api/athlete/```
 - [X] Mostrar atleta:  ```GET: api/athlete/{id}```
 - [X] Registrar atleta: ```POST: api/athlete/```
 - [X] Atualizar atleta:  ```PUT: api/athlete/{id}```
 - [X] Excluir atleta:  ```DELETE: api/athlete/{id}```
#### Team
 - [X] Listar times:  ```GET: api/team/```
 - [X] Mostrar time:  ```GET: api/team/{id}```
 - [X] Registrar time: ```POST: api/team/```
 - [X] Atualizar time:  ```PUT: api/team/{id}```
 - [X] Excluir time:  ```DELETE: api/team/{id}```
#### Sport
 - [X] Listar esportes:  ```GET: api/sport/```
 - [X] Mostrar esporte:  ```GET: api/sport/{id}```
 - [X] Registrar esporte: ```POST: api/sport/```
 - [X] Atualizar esporte:  ```PUT: api/sport/{id}```
 - [X] Excluir esporte:  ```DELETE: api/sport/{id}```
#### Pelada (Campeonatos)
 - [X] Listar campeonatos:  ```GET: api/pelada/```
 - [X] Mostrar campeonato:  ```GET: api/pelada/{id}```
 - [X] Registrar campeonato: ```POST: api/pelada/```
 - [X] Atualizar campeonato:  ```PUT: api/pelada/{id}```
 - [X] Excluir campeonato:  ```DELETE: api/pelada/{id}```
#### Team Manager
 - [X] Gerar times:  ```POST: api/team-manager/generate-teams/{peladaId}/{sportId}```
	 >Recurso principal da aplicação, este recurso gera os times do campeonato com base na relação entre quantidade de jogadores, tipo de esporte e quantidade de jogadores por equipe.
 


