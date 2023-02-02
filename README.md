##Teste aprovado Nubank


### 
###### Para visualizar melhor as informações contidas neste documento, recomendo copiar e colar todo este conteúdo no site Markdown Live Preview: [https://markdownlivepreview.com](https://markdownlivepreview.com/).

# Code Challenge: Ganho de Capital
[https://github.com/fredhfreitas/GanhoDeCapital/blob/master/spec-ptbr_(6).pdf)

Este projeto tem como objetivo apresentar um programa de linha de comando (CLI) que calcula o imposto a ser pago sobre lucros ou prejuízos de operações do mercado financeiro de ações.



## Arquitetura  

Utilizei o princípio do “Uncle Bob” Martin que recomenda fortemente à organização do projeto visando o fácil entendimento e que seja ágil a mudanças. 

Baseado na arquitetura limpa: 
```shell  
Ex: User Interface -> Domain -> Data Acess Library
```

#### Estrutura de pastas da aplicação 
```shell  
  .GanhoDeCapital
  ├── Business
  │   └── CalculadoraDeImpostos.cs (Implementa os calculos "Os métodos estão públicos apenas para facilitar a demonstração de testes unitários.")
  │   └── ICalculadoraDeImpostos.cs
  ├── Service
  │   └── ITransacaoService.cs
  │   └── TransacaoService.cs (Camada responsável por tratar a informação para a CalculadoraDeImpostos e retornar as taxas)  
  ├── Model (Classes para o tratamento de entrada e saída)
  │   └── Acao.cs
  │   └── Taxa.cs 
  ├── Util
  │   └── Util.cs (tratamento da entrada dos dados)
  ├── Dockerfile
  ├── Program.cs (Class principal onde tudo começa
  │
  .GanhoDeCapitalTeste
  ├── Business
  |   └── CalculadoraDeImpostosTest.cs (Contém os testes unitários)
  └── Cases
      └── CasosIntegrationTest.cs (Contém todos os 9 casos do code challenge)
      
```

```shell
Program -> TransacaoService -> CalculadoraDeImpostos
```

## Ferramentas utilizadas

- Visual Studio 2022 (necessário somente para compilação local)
- sdk .net3.1 https://dotnet.microsoft.com/en-us/download/dotnet/3.1 (necessário somente para compilação local)


## Instalação

Rodando a app em um conteiner docker. 

Após os arquivos estarem descompactados em uma pasta de sua preferência.

Na raíz da pasta do projeto execute os comandos.

```bash
  docker build -t ganhodecapital -f GanhoDeCapital\Dockerfile .
```

```bash
  docker run -it ganhodecapital
``` 

##### Após executar os comandos, copie e cole a entrada na app:

```json
    [{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
    {"operation":"sell", "unit-cost":50.00, "quantity": 10000},
    {"operation":"buy", "unit-cost":20.00, "quantity": 10000},
    {"operation":"sell", "unit-cost":50.00, "quantity": 10000}]
```

  ##### Espere a saída:

```json
    [{"tax":0.00},{"tax":80000.00},{"tax":0.00},{"tax":60000.00}]
```


##### Para rodar local é necessário baixar o sdk do .net 3.1 no link informado em "Ferramentas utilizadas"

Na raíz da aplicação dentro da pasta \GanhoDeCapital rode o comando:

```bash
  dotnet run
``` 

Para testes:
```bash
  dotnet test
``` 

Informe a entrada dos dados e espere a saída no console.



