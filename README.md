# Inventory Management System

Sistema simples de gerenciamento de estoque desenvolvido em C# como projeto de estudo.

O objetivo é praticar fundamentos de programação, orientação a objetos, encapsulamento, coleções, validações, tratamento de exceções e separação de responsabilidades.

## Funcionalidades

A Versão 1 permite:

- Cadastrar produtos.
- Listar todos os produtos cadastrados.
- Buscar um produto pelo código.
- Adicionar uma quantidade ao estoque.
- Retirar uma quantidade do estoque.
- Excluir um produto.
- Listar produtos com estoque abaixo do mínimo.
- Executar várias operações por meio de um menu interativo.

## Dados do produto

Cada produto possui:

- Código.
- Nome.
- Quantidade em estoque.
- Estoque mínimo.
- Preço.

## Regras de negócio

- Código e nome são obrigatórios.
- Espaços no início e no final do código e do nome são removidos.
- O código de cada produto deve ser único.
- A comparação de códigos não diferencia letras maiúsculas e minúsculas.
- Quantidade, estoque mínimo e preço não podem ser negativos.
- Entradas e retiradas devem possuir quantidade maior que zero.
- Uma retirada não pode ultrapassar a quantidade disponível.
- Um produto é considerado com estoque baixo quando sua quantidade é menor que o estoque mínimo.
- Operações inválidas exibem uma mensagem de erro sem encerrar a aplicação.

## Tecnologias utilizadas

- C#
- .NET 10
- Aplicação de console

O projeto não utiliza bibliotecas externas.

## Estrutura do projeto

```text
InventoryManagementSystem/
├── InventoryManagementSystem.slnx
└── InventoryManagementSystem.Console/
    ├── Models/
    │   └── Produto.cs
    ├── Services/
    │   └── ServicoEstoque.cs
    ├── Program.cs
    └── InventoryManagementSystem.Console.csproj
```

### Produto

Representa a entidade de domínio e concentra as validações relacionadas ao estado do produto e às movimentações de estoque.

### ServicoEstoque

Mantém a coleção de produtos e realiza operações como cadastro, busca, exclusão, listagem e consulta de estoque baixo.

### Program

Apresenta o menu, recebe as entradas do usuário, chama o serviço e exibe os resultados das operações.

## Como executar

### Pré-requisito

Instale o SDK do [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0).

### Clonar o repositório

```bash
git clone https://github.com/andreeliasgv/inventory-management-system.git
cd inventory-management-system
```

### Compilar

```bash
dotnet build InventoryManagementSystem/InventoryManagementSystem.slnx
```

### Executar

```bash
dotnet run --project InventoryManagementSystem/InventoryManagementSystem.Console
```

## Menu da aplicação

```text
1 - Cadastrar Produto
2 - Listar Produtos
3 - Buscar Produto por Código
4 - Adicionar Estoque
5 - Retirar Estoque
6 - Excluir Produto
7 - Listar Produtos com Estoque Baixo
8 - Sair
```

## Limitações da Versão 1

- Os dados são mantidos somente em memória.
- Os produtos são perdidos quando a aplicação é encerrada.
- Não há banco de dados ou armazenamento em arquivos.
- Não há interface gráfica, autenticação ou controle de usuários.
- Não há controle de fornecedores, vendas ou múltiplos depósitos.

## Possíveis evoluções

Depois da conclusão da Versão 1, o projeto poderá receber:

- Persistência em arquivos ou banco de dados.
- Edição dos dados dos produtos.
- Testes automatizados.
- Interface gráfica ou aplicação web.
- Relatórios mais detalhados.

## Licença

Consulte o arquivo [LICENSE](LICENSE) para obter informações sobre a licença do projeto.