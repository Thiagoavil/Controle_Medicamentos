using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados
    {
        private const string enderecoBanco =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=ControleMedicamentos.Projeto.SqlServer;" +
            "Integrated Security = True;" +
            "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO] 
                (
                    [FUNCIONARIO_ID],
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],
                    [DATA]
	            )
	            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,      
                    @MEDICAMENTO_ID,      
                    @QUANTIDADEMEDICAMENTO,      
                    @DATA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBREQUISICAO]	
		        SET
			        [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
                    [PACIENTE_ID] = @PACIENTE_ID,
                    [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                    [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
                    [DATA] = @DATA
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            REQUISICAO.[ID],
		            REQUISICAO.[QUANTIDADEMEDICAMENTO],
		            REQUISICAO.[DATA],
                    
                    PACIENTE.[ID] AS PACIENTE_ID,
                    PACIENTE.[NOME] AS PACIENTE_NOME,
                    PACIENTE.[CARTAOSUS] AS PACIENTE_CARTAOSUS,

                    FUNCIONARIO.[ID] AS FUNCIONARIO_ID,
                    FUNCIONARIO.[NOME] AS FUNCIONARIO_NOME,
                    FUNCIONARIO.[LOGIN] AS FUNCIONARIO_LOGIN,
                    FUNCIONARIO.[SENHA] AS FUNCIONARIO_SENHA,

                    MEDICAMENTO.[ID] AS MEDICAMENTO_ID,
                    MEDICAMENTO.[NOME] AS MEDICAMENTO_NOME,
                    MEDICAMENTO.[DESCRICAO] AS MEDICAMENTO_DESCRICAO,
                    MEDICAMENTO.[LOTE] AS MEDICAMENTO_LOTE,
                    MEDICAMENTO.[VALIDADE] AS MEDICAMENTO_VALIDADE,
                    MEDICAMENTO.[QUANTIDADEDISPONIVEL] AS MEDICAMENTO_QUANTIDADE,

                    FORNECEDOR.[ID] AS FORNECEDOR_ID,
                    FORNECEDOR.[NOME] AS FORNECEDOR_NOME,
                    FORNECEDOR.[TELEFONE] AS FORNECEDOR_TELEFONE,
                    FORNECEDOR.[EMAIL] AS FORNECEDOR_EMAIL,
                    FORNECEDOR.[CIDADE] AS FORNECEDOR_CIDADE,
                    FORNECEDOR.[ESTADO] AS FORNECEDOR_ESTADO
		          
	            FROM 
		            [TBREQUISICAO] AS REQUISICAO INNER JOIN
                    [TBFUNCIONARIO] AS FUNCIONARIO
                ON
                    REQUISICAO.[FUNCIONARIO_ID] = FUNCIONARIO.[ID]
                    INNER JOIN [TBPACIENTE] AS PACIENTE
                ON
                    REQUISICAO.[PACIENTE_ID] = PACIENTE.[ID]
                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                ON
                    REQUISICAO.[MEDICAMENTO_ID] = MEDICAMENTO.[ID]
                    INNER JOINE [TBFORNECEDOR] AS FORNECEDOR
                ON
                    MEDICAMENTO.[FORNECEDOR_ID]=FORNECEDOR.[ID]";

        private const string sqlSelecionarPorNumero =
          @"SELECT 
		            REQUISICAO.[ID],
		            REQUISICAO.[QUANTIDADEMEDICAMENTO],
		            REQUISICAO.[DATA],
                    
                    PACIENTE.[ID] AS PACIENTE_ID,
                    PACIENTE.[NOME] AS PACIENTE_NOME,
                    PACIENTE.[CARTAOSUS] AS PACIENTE_CARTAOSUS,

                    FUNCIONARIO.[ID] AS FUNCIONARIO_ID,
                    FUNCIONARIO.[NOME] AS FUNCIONARIO_NOME,
                    FUNCIONARIO.[LOGIN] AS FUNCIONARIO_LOGIN,
                    FUNCIONARIO.[SENHA] AS FUNCIONARIO_SENHA,

                    MEDICAMENTO.[ID] AS MEDICAMENTO_ID,
                    MEDICAMENTO.[NOME] AS MEDICAMENTO_NOME,
                    MEDICAMENTO.[DESCRICAO] AS MEDICAMENTO_DESCRICAO,
                    MEDICAMENTO.[LOTE] AS MEDICAMENTO_LOTE,
                    MEDICAMENTO.[VALIDADE] AS MEDICAMENTO_VALIDADE,
                    MEDICAMENTO.[QUANTIDADEDISPONIVEL] AS MEDICAMENTO_QUANTIDADE,

                    FORNECEDOR.[ID] AS FORNECEDOR_ID,
                    FORNECEDOR.[NOME] AS FORNECEDOR_NOME,
                    FORNECEDOR.[TELEFONE] AS FORNECEDOR_TELEFONE,
                    FORNECEDOR.[EMAIL] AS FORNECEDOR_EMAIL,
                    FORNECEDOR.[CIDADE] AS FORNECEDOR_CIDADE,
                    FORNECEDOR.[ESTADO] AS FORNECEDOR_ESTADO
		          
	            FROM 
		            [TBREQUISICAO] AS REQUISICAO INNER JOIN
                    [TBFUNCIONARIO] AS FUNCIONARIO
                ON
                    REQUISICAO.[FUNCIONARIO_ID] = FUNCIONARIO.[ID]
                    INNER JOIN [TBPACIENTE] AS PACIENTE
                ON
                    REQUISICAO.[PACIENTE_ID] = PACIENTE.[ID]
                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                ON
                    REQUISICAO.[MEDICAMENTO_ID] = MEDICAMENTO.[ID]
                    INNER JOIN [TBFORNECEDOR] AS FORNECEDOR
                ON
                    MEDICAMENTO.[FORNECEDOR_ID]=FORNECEDOR.[ID]
                WHERE
                    REQUISICAO.[ID] = @ID";

        #endregion

        public ValidationResult Inserir(Requisicao novaRequisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(novaRequisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(novaRequisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaRequisicao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }

        public Requisicao SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicao = null;
            if (leitorRequisicao.Read())
                requisicao = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicao;
        }

        private static Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            int idRequisicao = Convert.ToInt32(leitorRequisicao["ID"]);
            int quantidadeMedicamentos = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            DateTime dataRequisicao = Convert.ToDateTime(leitorRequisicao["DATA"]);
            
            int idPaciente = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            string nomePaciente = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);
            string cartaoSus = Convert.ToString(leitorRequisicao["PACIENTE_CARTAOSUS"]);

            int idFuncionario = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            string nomeFuncionario = Convert.ToString(leitorRequisicao["FUNCIONARIO_NOME"]);
            string loginFuncionario = Convert.ToString(leitorRequisicao["FUNCIONARIO_LOGIN"]);
            string senhaFuncionario = Convert.ToString(leitorRequisicao["FUNCIONARIO_SENHA"]);

            int idMedicamento = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            string nomeMedicamento = Convert.ToString(leitorRequisicao["MEDICAMENTO_NOME"]);
            string descricaoMedicamento = Convert.ToString(leitorRequisicao["MEDICAMENTO_DESCRICAO"]);
            string loteMedicamento = Convert.ToString(leitorRequisicao["MEDICAMENTO_LOTE"]);
            DateTime validadeMedicamento = Convert.ToDateTime(leitorRequisicao["MEDICAMENTO_VALIDADE"]);
            int quantidadeMedicamentoDisponivel = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_QUANTIDADE"]);

            int idFornecedor = Convert.ToInt32(leitorRequisicao["FORNECEDOR_ID"]);
            string nomeFornecedor = Convert.ToString(leitorRequisicao["FORNECEDOR_NOME"]);
            string telefoneFornecedor = Convert.ToString(leitorRequisicao["FORNECEDOR_TELEFONE"]);
            string emailFornecedor = Convert.ToString(leitorRequisicao["FORNECEDOR_EMAIL"]);
            string cidadeFornecedor = Convert.ToString(leitorRequisicao["FORNECEDOR_CIDADE"]);
            string estadoFornecedor = Convert.ToString(leitorRequisicao["FORNECEDOR_ESTADO"]);

            var fornecedor = new Fornecedor(nomeFornecedor,telefoneFornecedor,emailFornecedor,cidadeFornecedor,estadoFornecedor)
            {
                Id = idFornecedor,
            };
            var paciente = new Paciente(nomePaciente,cartaoSus)
            { 
                Id= idPaciente,
            };

            var funcionario = new Funcionario(nomeFuncionario,loginFuncionario,senhaFuncionario)
            { 
                Id = idFuncionario
            };

            var medicamento = new Medicamento(nomeMedicamento, descricaoMedicamento, loteMedicamento, validadeMedicamento)
            {
                Fornecedor=fornecedor,
                Id = idMedicamento
            };

            var requisicao = new Requisicao()
            {
                Id = idRequisicao,
                QtdMedicamento = quantidadeMedicamentos,
                Data = dataRequisicao,
            };
            requisicao.InserirMedicamento(medicamento);
            requisicao.InserirFuncionario(funcionario);
            requisicao.InserirPaciente(paciente);

            return requisicao;
        }

        private static void ConfigurarParametrosRequisicao(Requisicao novaRequisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novaRequisicao.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", novaRequisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", novaRequisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", novaRequisicao.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", novaRequisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", novaRequisicao.Data);
        }
    }
}
