using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem.SqlServer.Data
{
	public class ConexaoSqlServer
	{
		private readonly string _stringConexao;

		public ConexaoSqlServer(string stringConexao)
		{
			if (string.IsNullOrWhiteSpace(stringConexao))
			{
				throw new ArgumentException(
					"A string de conexão deve ser informada!",
					nameof(stringConexao));
			}

			_stringConexao = stringConexao;
		}

		public SqlConnection CriarConexao()
		{
			return new SqlConnection(_stringConexao);
		}
	}
}