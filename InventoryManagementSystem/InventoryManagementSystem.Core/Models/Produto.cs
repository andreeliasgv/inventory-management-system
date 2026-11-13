using InventoryManagementSystem.Core.Enums.Produto;

namespace InventoryManagementSystem.Core.Models
{
	public class Produto
	{
		private string _codigo = string.Empty;
		private string _nome = string.Empty;
		private decimal _quantidade;
		private decimal _estoqueMinimo;
		private decimal _preco;
		private StatusProduto _status;


		public int Id
		{ get; private set; }

		public string Codigo
		{
			get { return _codigo; }
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("O código do produto deve ser informado!");
				}

				string codigo = value.Trim().ToUpperInvariant();

				if (codigo.Length > 50)
				{
					throw new ArgumentException("O código deverá conter até 50 caracteres.");
				}

				_codigo = codigo;
			}
		}
		public string Nome
		{
			get { return _nome; }
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("O nome do produto deve ser informado!");
				}

				string nome = value.Trim();

				if (nome.Length > 150)
				{
					throw new ArgumentException("O nome não pode ultrapassar 150 caracteres.");
				}

				_nome = nome;
			}
		}
		public decimal Quantidade
		{
			get { return _quantidade; }
			private set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(
						nameof(value),
						value,
						"A quantidade não pode ser negativa!");
				}

				_quantidade = value;
			}
		}

		public decimal EstoqueMinimo
		{
			get
			{
				return _estoqueMinimo;
			}
			private set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(
						nameof(value),
						value,
						"A quantidade mínima não pode ser negativa!");
				}

				_estoqueMinimo = value;
			}
		}

		public decimal Preco
		{
			get { return _preco; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(
						nameof(value),
						value,
						"O preço não pode ser negativo!");
				}
				_preco = value;
			}
		}

		public DateTime DataCadastro
		{ get; private set; }

		public StatusProduto Status
		{
			get { return _status; }
			private set
			{
				if (!Enum.IsDefined(typeof(StatusProduto), value))
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "O status informado é inválido");
				}

				_status = value;
			}
		}

		public Produto(string codigo, string nome, decimal quantidade, decimal estoqueMinimo, decimal preco)
		{
			Codigo = codigo;
			Nome = nome;
			Quantidade = quantidade;
			EstoqueMinimo = estoqueMinimo;
			Preco = preco;
			Status = StatusProduto.Ativo;
		}

		public void AdicionarEstoque(decimal quantidade)
		{
			if (quantidade <= 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(quantidade),
					quantidade,
					"A quantidade a ser adicionada deve ser maior que zero!");
			}

			Quantidade += quantidade;
		}
		public void RemoverEstoque(decimal quantidade)
		{
			if (quantidade <= 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(quantidade),
					quantidade,
					"A quantidade a ser removida deve ser maior que zero!");
			}

			if (quantidade > Quantidade)
			{
				throw new InvalidOperationException("Não há estoque suficiente para a retirada");
			}

			Quantidade -= quantidade;
		}

		public void AlterarEstoqueMinimo(decimal novoEstoqueMinimo)
		{
			EstoqueMinimo = novoEstoqueMinimo;
		}

		public void Ativar()
		{
			Status = StatusProduto.Ativo;
		}

		public void Inativar()
		{
			if (Quantidade != 0)
			{
				throw new InvalidOperationException("Um produto com saldo em estoque não pode ser inativado!");
			}

			Status = StatusProduto.Inativo;
		}

		public override string ToString()
		{
			return $"Código: {Codigo}\n"
				+ $"Nome: {Nome}\n"
				+ $"Quantidade: {Quantidade}\n"
				+ $"Estoque mínimo: {EstoqueMinimo}\n"
				+ $"Preço: {Preco}\n";
		}
	}
}
