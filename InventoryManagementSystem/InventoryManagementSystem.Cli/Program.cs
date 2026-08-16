using InventoryManagementSystem.Core.Models;
using InventoryManagementSystem.Core.Services;

namespace InventoryManagementSystem.Cli
{
	class Program
	{
		private static string Read(string? mensagem = null, bool quebrarLinha = false)
		{
			if (mensagem != null)
			{
				if (quebrarLinha)
				{
					Console.WriteLine(mensagem);
				}
				else
				{
					Console.Write(mensagem);
				}
			}

			return Console.ReadLine();
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
					Console.Write("\nDigite um valor válido!\n\n> ");

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
					Console.Write("\nDigite um valor válido!\n\n> ");

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
				Console.WriteLine("\nNão existem produtos cadastrados.");
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
				Console.WriteLine("=======================================");
				Console.WriteLine("===== Inventory Management System =====");
				Console.WriteLine("=======================================");
				Console.WriteLine("Selecione uma opção:");
				Console.WriteLine();
				Console.WriteLine("1 - Cadastrar Produto");
				Console.WriteLine("2 - Listar Produtos");
				Console.WriteLine("3 - Buscar Produto por Código");
				Console.WriteLine("4 - Adicionar Estoque");
				Console.WriteLine("5 - Retirar Estoque");
				Console.WriteLine("6 - Excluir Produto");
				Console.WriteLine("7 - Listar Produtos com Estoque Baixo");
				Console.WriteLine("8 - Sair");
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
							Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							Console.WriteLine();
							nome = Read("Nome do produto:\n> ");
							Console.WriteLine();
							quantidade = ReadDecimal(Read("Quantidade em estoque:\n> "));
							Console.WriteLine();
							estoqueMinimo = ReadDecimal(Read("Estoque mínimo:\n> "));
							Console.WriteLine();
							preco = ReadDecimal(Read("Preço do produto:\n> "));
							Console.WriteLine();

							produto = new(codigo, nome, quantidade, estoqueMinimo, preco);
							estoque.CadastrarProduto(produto);

							Thread.Sleep(150);
							Console.WriteLine("Produto cadastrado com sucesso!");
							break;
						case 2:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							List<Produto> produtos = estoque.ListarProdutos();

							Console.WriteLine();
							for (int i = 0; i < produtos.Count; i++)
							{
								Console.WriteLine("=======================================");
								Console.WriteLine($" ============= PRODUTO {i + 1} ============= ");
								Console.WriteLine("=======================================");
								Console.WriteLine(produtos[i].ToString());
							}
							Console.WriteLine("=======================================");
							break;
						case 3:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							codigo = Read("Informe o código do produto:\n> ");

							produto = estoque.BuscarPorCodigo(codigo);

							if (produto == null)
							{
								Console.WriteLine("\nNenhum produto encontrado.");
							}
							else
							{
								Console.WriteLine(produto.ToString());
							}
							break;
						case 4:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							quantidade = ReadDecimal(Read("Quantidade a adicionar:\n> "));
							estoque.AdicionarEstoque(codigo, quantidade);
							break;
						case 5:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							quantidade = ReadDecimal(Read("Quantidade a retirar:\n> "));
							estoque.RemoverEstoque(codigo, quantidade);
							break;
						case 6:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							codigo = Read("Codigo do produto:\n> ");
							estoque.ExcluirProduto(codigo);
							break;
						case 7:
							if (!ValidarProdutoCadastrado(estoque)) break;

							Console.Clear();
							List<Produto> produtosEstoqueBaixo = estoque.ListarEstoqueBaixo();

							if (produtosEstoqueBaixo.Count > 0)
							{
								Console.WriteLine();
								Console.WriteLine("======================================");
								if (produtosEstoqueBaixo.Count > 1)
								{
								Console.WriteLine($" ======= {produtosEstoqueBaixo.Count} PRODUTOS EM FALTA ======== ");
								}
								else
								{
								Console.WriteLine($" ======== {produtosEstoqueBaixo.Count} PRODUTO EM FALTA ======== ");
								}
								Console.WriteLine("======================================");
								for (int i = 0; i < produtosEstoqueBaixo.Count; i++)
								{
									Console.WriteLine("======================================");
									Console.WriteLine(produtosEstoqueBaixo[i].ToString());
								}
								Console.WriteLine("=======================================");
							}
							else
							{
								Console.WriteLine("Não existem produtos com estoque baixo.");
							}
							break;
						case 8:
							Console.Clear();
							continuar = false;
							break;
						default:
							Console.WriteLine();
							Console.WriteLine("Informe uma opção válida!");
							break;
					}
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine($"\nErro: {ex.Message}");
				}
				catch (InvalidOperationException ex)
				{
					Console.WriteLine($"\nErro: {ex.Message}");
				}

				if (continuar)
				{
					Console.WriteLine();
					Thread.Sleep(150);
					Console.Write("Pressione qualquer tecla para prosseguir...");
					Console.ReadKey(true);
					Thread.Sleep(150);
					Console.Clear();
				}
			}
		}
	}
}