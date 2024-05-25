# Desafio Final WebApi com Testes da DIO

## Sobre
Desafio feito como último exercício do Bootcamp Coding Future Avanade .Net Develope.

## No que consiste o projeto
O Projeto é uma Api que permite uma pessoa poder cadastrar uma lista de tarefas e colocar uma lista de responsáveis por tarefa.

## Implementação

### Relacionamento

Esse projeto foi pensado num relacionamento de **N:N** entre **Pessoa** e **Tarefa**. Ou seja, uma **Tarefa** pode ser realizada por várias **Pessoas** e uma **Pessoa** pode realizar várias **Tarefas**.

### Modelos

**Tarefa:**

```C#
int Id;
string Titulo;
string Descricao;
EnumStatusTarefas Status;
DateTime DataCriacao;
DateTime DataInicio;
DateTime DataConclusao;
List<Pessoa> Responsaveis;
```
<br/>

**Enum Status**
```C#
Pendente,
EmAndamento,
Concluida
```
<br/>

**Pessoa**
```C#
int Id;
string Nome;
```


### Endpoints

**Tarefa:**

| Verbo  | Endpoint                    | Parâmetro | Body          |
|--------|-----------------------------|-----------|---------------|
| GET    | /api/tarefa                 | N/A       | N/A           |
| GET    | /api/tarefa/{id}            | id        | N/A           |
| POST   | /api/tarefa                 | N/A       | Tarefa        |
| PATCH  | /api/tarefa/{id}            | id        | Tarefa        |
| DELETE | /api/tarefa/{id}            | id        | N/A           |
| PATCH  | /api/tarefa/{id}/status     | N/A    | Status da Tarefa |
| POST   | /api/tarefa/responsavel     | N/A       | TarefaPessoa  |
| DELETE | /api/tarefa/responsavel     | N/A       | TarefaPessoa  |

<br/>

**Pessoa:**

| Verbo  | Endpoint                    | Parâmetro | Body          |
|--------|-----------------------------|-----------|---------------|
| GET    | /api/pessoa                 | N/A       | N/A           |
| GET    | /api/pessoa/{id}            | id        | N/A           |
| POST   | /api/pessoa                 | N/A       | Pessoa        |
| PUT    | /api/pessoa/{id}            | id        | Pessoa        |
| DELETE | /api/pessoa/{id}            | id        | N/A           |


### Json que podem ser enviados no body

**Tarefa**
```json
{
  "titulo": "Tarefa 5",
  "descricao": "Descrição da tarefa 5",
  "status": "Pendente"
}
```
<br/>

**Status da Tarefa**
```json
{
  "status": "Concluído"
}
```
<br/>

**Status da TarefaPessoa**
```json
{
  "tarefaId": 3,
  "pessoaId": 3
}
```
<br/>

**Pessoa**
```json
{
  "nome": "Maria Eduarda Mendes"
}
```

### Como executar a aplicação


**Aplicação**
```bash
cd Tarefas
dotnet watch run
```
<br/>

**Testes**
```bash
cd TarefasTestes
dotnet tests
```

***
**Observações**
<br/>
Não se esqueça de rodar as migrations caso necessário

```bash
dotnet ef database update
```

Não se esqueça de modificar o arquivo appsettings.Development.json e colocar suas próprias configurações de Conexão com o banco de dados.


### Observações finais
O projeto faz uso do Swagger para auxiliar na execução das requisições, mas também pode se usar o arquivo Tarefas.http