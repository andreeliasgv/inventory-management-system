using InventoryManagementSystem.Console.Models;
using InventoryManagementSystem.Console.Services;

namespace InventoryManagementSystem
{
	class Program
	{
		private static string Read(string? mensagem = null, bool quebrarLinha = false)
		{
			if (mensagem != null)
			{
				if (quebrarLinha)
				{
					System.Console.WriteLine(mensagem);
				}
				else
				{
					System.Console.Write(mensagem);
				}
			}

			return System.Console.ReadLine();
		}

		private static int ReadInt(string texto)
		{
			if (int.TryParse(texto, out int valorInteiro))
			{
				return valorInteiro;
			}
			else
			{
				while (true)
				{
					System.Console.Write("\nDigite um valor válido!\n\n> ");

					if (int.TryParse(Read(), out valorInteiro))
					{
						return valorInteiro;
					}
				}
			}
		}
		private static decimal ReadDecimal(string texto)
		{
			if (decimal.TryParse(texto, out decimal valorDecimal))
			{
				return valorDecimal;
			}
			else
			{
				while (true)
				{
					System.Console.Write("\nDigite um valor válido!\n\n> ");

					if (decimal.TryParse(Read(), out valorDecimal))
					{
						return valorDecimal;
					}
				}
			}
		}

		private static bool ValidarProdutoCadastrado(ServicoEstoque estoque)
		{
			if (estoque.ListarProdutos().Count == 0)
			{
				System.Console.WriteLine("\nNão existem produtos cadastrados.");
				return false;
			}

			return true;
		}

		public static void Main(String[] args)
		{
			Produto? produto = null;
			ServicoEstoque estoque = new();
			bool continuar = true;

			while (continuar)
			{
				System.Console.WriteLine("=======================================");
				System.Console.WriteLine("===== Inventory Management System =====");
				System.Console.WriteLine("=======================================");
				System.Console.WriteLine("Selecione uma opção:");
				System.Console.WriteLine();
				System.Console.WriteLine("1 - Cadastrar Produto");
				System.Console.WriteLine("2 - Listar Produtos");
				System.Console.WriteLine("3 - Buscar Produto por Código");
				System.Console.WriteLine("4 - Adicionar Estoque");
				System.Console.WriteLine("5 - Retirar Estoque");
				System.Console.WriteLine("6 - Excluir Produto");
				System.Console.WriteLine("7 - Listar Produtos com Estoque Baixo");
				System.Console.WriteLine("8 - Sair");
				int nOpcaoMenu = 0;
				try
				{
					nOpcaoMenu = ReadInt(Read("\n> "));
				}
				catch (FormatException)
				{
					nOpcaoMenu = 0;
				}

				try
				{
					switch (nOpcaoMenu)
					{
						case 1:
							string codigo, nome;
							decimal quantidade, estoqueMinimo, preco;
							System.Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							System.Console.WriteLine();
							nome = Read("Nome do produto:\n> ");
							System.Console.WriteLine();
							quantidade = ReadDecimal(Read("Quantidade em estoque:\n> "));
							System.Console.WriteLine();
							estoqueMinimo = ReadDecimal(Read("Estoque mínimo:\n> "));
							System.Console.WriteLine();
							preco = ReadDecimal(Read("Preço do produto:\n> "));
							System.Console.WriteLine();

							produto = new(codigo, nome, quantidade, estoqueMinimo, preco);
							estoque.CadastrarProduto(produto);

							Thread.Sleep(150);
							System.Console.WriteLine("Produto cadastrado com sucesso!");
							break;
						case 2:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							List<Produto> produtos = estoque.ListarProdutos();

							System.Console.WriteLine();
							for (int i = 0; i < produtos.Count; i++)
							{
								System.Console.WriteLine("=======================================");
								System.Console.WriteLine($" ============= PRODUTO {i + 1} ============= ");
								System.Console.WriteLine("=======================================");
								System.Console.WriteLine(produtos[i].ToString());
							}
							System.Console.WriteLine("=======================================");
							break;
						case 3:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							codigo = Read("Informe o código do produto:\n> ");

							produto = estoque.BuscarPorCodigo(codigo);

							if (produto == null)
							{
								System.Console.WriteLine("\nNenhum produto encontrado.");
							}
							else
							{
								System.Console.WriteLine(produto.ToString());
							}
							break;
						case 4:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							quantidade = ReadDecimal(Read("Quantidade em estoque:\n> "));
							estoque.AdicionarEstoque(codigo, quantidade);
							break;
						case 5:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							quantidade = ReadDecimal(Read("Quantidade em estoque:\n> "));
							estoque.RemoverEstoque(codigo, quantidade);
							break;
						case 6:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							estoque.ExcluirProduto(codigo);
							break;
						case 7:
							if (!ValidarProdutoCadastrado(estoque)) break;

							System.Console.Clear();
							List<Produto> produtosEstoqueBaixo = estoque.ListarEstoqueBaixo();

							if (produtosEstoqueBaixo.Count > 0)
							{
								System.Console.WriteLine();
								for (int i = 0; i < produtosEstoqueBaixo.Count; i++)
								{
									System.Console.WriteLine("======================================");
									System.Console.WriteLine($" ======== PRODUTO {i + 1} EM FALTA ======== ");
									System.Console.WriteLine("======================================");
									System.Console.WriteLine(produtosEstoqueBaixo[i].ToString());
								}
								System.Console.WriteLine("=======================================");
							}
							else
							{
								System.Console.WriteLine("Não existem produtos com estoque baixo.");
							}
							break;
						case 8:
							System.Console.Clear();
							continuar = false;
							break;
						default:
							System.Console.WriteLine();
							System.Console.WriteLine("Informe uma opção válida!");
							break;
					}
				}
				catch (ArgumentException ex)
				{
					System.Console.WriteLine($"\nErro: {ex.Message}");
				}
				catch (InvalidOperationException ex)
				{
					System.Console.WriteLine($"\nErro: {ex.Message}");
				}

				if (continuar)
				{
					System.Console.WriteLine();
					Thread.Sleep(150);
					Read("Pressione Enter para prosseguir...");
					Thread.Sleep(150);
					System.Console.Clear();
				}
			}
		}
	}
}