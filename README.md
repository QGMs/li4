# CyberForgeMS

Projeto desenvolvido no âmbito da unidade curricular de Laboratórios de Informática IV.

O **CyberForgeMS** é um sistema de gestão desenvolvido em C#/.NET, focado na gestão de encomendas, produtos, stock, fornecedores, utilizadores e linhas de montagem.

## Contribuidores

* David Figueiredo - a104360
* Diogo Ferreira - a104266
* João Pedro Carvalho - a104533

## Descrição

Este projeto consiste numa aplicação de gestão para uma empresa de montagem e venda de produtos. O sistema permite gerir produtos, componentes, fornecedores, encomendas, utilizadores e o processo de produção associado às linhas de montagem.

A aplicação organiza a lógica em diferentes camadas, separando os modelos de negócio, o acesso a dados e a interface com o utilizador.

## Funcionalidades

* Gestão de utilizadores.
* Gestão de produtos.
* Gestão de componentes.
* Gestão de fornecedores.
* Gestão de stock.
* Criação e consulta de encomendas.
* Associação de produtos a encomendas.
* Cálculo de preço e tempo total das encomendas.
* Gestão de linhas de montagem.
* Acompanhamento do estado de produção.
* Integração com base de dados através de DAOs.

## Tecnologias utilizadas

* C#
* .NET
* ASP.NET
* Razor Pages
* SQL
* HTML/CSS
* Arquitetura em camadas

## Estrutura do projeto

```text id="k0px12"
.
├── README.md
├── Business
│   ├── Encomendas
│   ├── LinhaMontagem
│   ├── Stock
│   └── Utilizadores
├── Data
│   ├── Encomendas
│   ├── Stock
│   └── Utilizadores
├── Pages
├── wwwroot
└── Program.cs
```

## Principais módulos

### Encomendas

Responsável pela criação, consulta e atualização de encomendas. Cada encomenda pode conter vários produtos, quantidades, preço total e tempo estimado de montagem.

### Produtos

Representa os produtos disponíveis no sistema, incluindo preço de venda, preço de fabrico, componentes necessários e tempo de montagem.

### Stock

Permite gerir componentes, fornecedores e quantidades disponíveis para produção.

### Linha de montagem

Responsável pelo acompanhamento do processo de montagem dos produtos, incluindo componentes por montar, componentes já montados, produtos por fazer e estado da encomenda.

### Utilizadores

Módulo dedicado à gestão de utilizadores e funcionários, incluindo dados pessoais, autenticação e associação a linhas de montagem.

## Como executar

Para executar o projeto, é necessário ter o SDK do .NET instalado.

Na raiz do projeto, usar:

```bash id="qf7h2m"
dotnet restore
```

Depois:

```bash id="kxtd94"
dotnet build
```

E para iniciar a aplicação:

```bash id="mz2f7a"
dotnet run
```

## Estado do projeto

O projeto contém a implementação base de um sistema de gestão empresarial, com foco em encomendas, stock, produtos, fornecedores, utilizadores e linhas de montagem.
