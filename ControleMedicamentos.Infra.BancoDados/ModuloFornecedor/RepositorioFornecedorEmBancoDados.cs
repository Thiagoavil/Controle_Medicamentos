using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    internal class RepositorioFornecedorEmBancoDados
    {
         private const string enderecoBanco= 
            "Data Source=(localdb)\\MSSQLLocalDB;"+
            "Initial Catalog ControleMedicamentos.Projeto.SqlServer;"+
            "Integrated Security = True;"+ 
            "Pooling=False";

        #region sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBFORNECEDOR] 
                (
                    [NOME],
                    [TELEFONE],
                    [EMAIL],
                    [CIDADE],
                    [ESTADO]
	            )
	            VALUES
                (
                    @NOME,
                    @TELEFONE,
                    @EMAIL,
                    @CIDADE,
                    @ESTADO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBFORNECEDOR]	
		        SET
			        [NOME] = @NOME,
			        [TELEFONE] = @TELEFONE,
			        [EMAIL] = @EMAIL,
			        [CIDADE] = @CIDADE,
			        [ESTADO] = @ESTADO
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBFORNECEDOR]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [ID], 
		            [NOME], 
		            [TELEFONE],
		            [EMAIL],
		            [CIDADE],
		            [ESTADO]
	            FROM 
		            [TBFORNECEDOR]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [ID], 
		            [NOME], 
		            [TELEFONE],
		            [EMAIL],
		            [CIDADE],
		            [ESTADO]
	            FROM 
		            [TBFORNECEDOR]
		        WHERE   
                    [ID] = @ID";

        #endregion

        public ValidationResult Inserir(Fornecedor novoFornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(novoFornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFornecedor(novoFornecedor, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoFornecedor.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Fornecedor fornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFornecedor(fornecedor, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> fornecedores = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor fornecedor = ConverterParaContato(leitorFornecedor);

                fornecedores.Add(fornecedor);
            }

            conexaoComBanco.Close();

            return fornecedores;
        }

        public Fornecedor SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            Fornecedor fornecedor = null;
            if (leitorFornecedor.Read())
                fornecedor = ConverterParaContato(leitorFornecedor);

            conexaoComBanco.Close();

            return fornecedor;
        }

        private static Fornecedor ConverterParaContato(SqlDataReader leitorFuncionario)
        {
            int numero = Convert.ToInt32(leitorFuncionario["ID"]);
            string nome = Convert.ToString(leitorFuncionario["NOME"]);
            string telefone = Convert.ToString(leitorFuncionario["TELEFONE"]);
            string email = Convert.ToString(leitorFuncionario["EMAIL"]);
            string cidade = Convert.ToString(leitorFuncionario["CIDADE"]);
            string estado = Convert.ToString(leitorFuncionario["ESTADO"]);

            var fornecedor = new Fornecedor(nome,telefone,email,cidade,estado)
            {
                Id = numero,
                Nome = nome,
                Telefone = telefone,
                Email = email,
                Cidade = cidade,
                Estado = estado
            };

            return fornecedor;
        }

        private static void ConfigurarParametrosFornecedor(Fornecedor novoFornecedor, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoFornecedor.Id);
            comando.Parameters.AddWithValue("NOME", novoFornecedor.Nome);
            comando.Parameters.AddWithValue("TELEFONE", novoFornecedor.Telefone);
            comando.Parameters.AddWithValue("EMAIL", novoFornecedor.Email);
            comando.Parameters.AddWithValue("CIDADE", novoFornecedor.Cidade);
            comando.Parameters.AddWithValue("ESTADO", novoFornecedor.Estado);
        }

    }
}
