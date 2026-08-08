using InventoryManagementSystem.Console.Models;

namespace InventoryManagementSystem.Console.Services
{
	internal class ServicoEstoque
	{
		private readonly List<Produto> _listaProdutos = new();

		public Produto? BuscarPorCodigo(string codigo)
		{
			if (String.IsNullOrWhiteSpace(codigo))
			{
				throw new ArgumentException("O código do produto deve ser informado!", nameof(codigo));
			}

			codigo = codigo.Trim();

			foreach (Produto produto in _listaProdutos)
			{
				if (string.Equals(codigo, produto.Codigo, StringComparison.OrdinalIgnoreCase))
				{
					return produto;
				}
			}


			return null;
		}

		public void CadastrarProduto(Produto produto)
		{
			if (produto == null)
			{
				throw new ArgumentNullException(nameof(produto), "O produto deve ser informado!");
			}
			if (BuscarPorCodigo(produto.Codigo) != null)
			{
				throw new InvalidOperationException("O código informado já está em uso!");
			}

			_listaProdutos.Add(produto);
		}

		public void ExcluirProduto(string codigoProduto)
		{
			if (String.IsNullOrWhiteSpace(codigoProduto))
			{
				throw new ArgumentNullException(nameof(codigoProduto), "O código do produto deve ser informado!");
			}

			Produto? produto = BuscarPorCodigo(codigoProduto);

			if (produto == null)
			{
				throw new InvalidOperationException("O produto informado não está cadastrado!");
			}

			_listaProdutos.Remove(produto);
		}

		public List<Produto> ListarProdutos()
		{
			return _listaProdutos;
		}

		public void AdicionarEstoque(string codigoProduto, decimal quantidade)
		{
			Produto produto = BuscarPorCodigo(codigoProduto);

			if (produto == null)
			{
				throw new InvalidOperationException("O produto informado não está cadastrado!");
			}

			produto.AdicionarEstoque(quantidade);
		}

		public void RemoverEstoque(string codigoProduto, decimal quantidade)
		{
			Produto produto = BuscarPorCodigo(codigoProduto);

			if (produto == null)
			{
				throw new InvalidOperationException("O produto informado não está cadastrado!");
			}

			produto.RemoverEstoque(quantidade);
		}

		public List<Produto> ListarEstoqueBaixo()
		{
			List<Produto>? produtosEstoqueBaixo = new();

			foreach (Produto produto in _listaProdutos)
			{
				if (produto.Quantidade < produto.EstoqueMinimo)
				{
					produtosEstoqueBaixo.Add(produto);
				}
			}

				return produtosEstoqueBaixo;
		}
	}
}
