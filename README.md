# Seu Pet

Projeto de adoção de pets.

### 📋 Pré-requisitos

```
Sdk dotnet 8.0
Postgres
```

### 🔧 Instalação

Com o projeto clonado usar os seguintes comandos:
```
dotnet restore
dotnet build
dotnet run
```
Usando como o exemplo o endpoint localhost:PORT/adotantes, obterá o seguinte resultado:
```
	{
		"statusCode": 200,
		"success": true,
		"data": [
			{
				"id": int,
				"nome": string,
				"dataNascimento": "string",
				"sexo": "string",
				"tipo": "string",
				"foto": "string"
			},
			{
	      			"id": int,
				"nome": string,
				"dataNascimento": "string",
				"sexo": "string",
				"tipo": "string",
				"foto": "string"
			}
		],
		"errors": null
	}
```

## 🛠️ Construído com

* [Dotnet]([http://www.dropwizard.io/1.0.2/docs/](https://dotnet.microsoft.com/pt-br/)) - .NET 8.0
* [Postgres]([https://maven.apache.org/](https://www.postgresql.org/)) - Postgres

