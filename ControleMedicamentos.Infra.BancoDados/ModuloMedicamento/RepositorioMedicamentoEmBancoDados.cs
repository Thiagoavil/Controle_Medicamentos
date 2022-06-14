using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    internal class RepositorioMedicamentoEmBancoDados
    {
        private const string enderecoBanco =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog ControleMedicamentos.Projeto.SqlServer;" +
            "Integrated Security = True;" +
            "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBMEDICAMENTOS] 
                (
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            )
	            VALUES
                (
                    @NOME,
                    @DESCRICAO,      
                    @LOTE,      
                    @VALIDADE,      
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBMEDICAMENTOS]	
		        SET
			        [NOME] = @NOME
                    [DESCRICAO] = @DESCRICAO
                    [LOTE] = @LOTE
                    [VALIDADE] = @VALIDADE
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL
                    [FORNECEDOR_ID] = @FORNECEDOR_ID
			       
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTOS]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            MEDICAMENTO.[ID],
		            MEDICAMENTO.[NOME],
		            MEDICAMENTO.[DESCRICAO],
		            MEDICAMENTO.[LOTE],
		            MEDICAMENTO.[VALIDADE],
		            MEDICAMENTO.[QUANTIDADEDISPONIVEL],
                    
                    FORNECEDOR.[ID] AS FORNECEDOR_NUMERO,
                    FORNECEDOR.[NOME] AS FORNECEDOR_NOME,
                    FORNECEDOR.[TELEFONE] AS FORNECEDOR_TELEFONE,
                    FORNECEDOR.[EMAIL] AS FORNECEDOR_EMAIL,
                    FORNECEDOR.[CIDADE] AS FORNECEDOR_CIDADE,
                    FORNECEDOR.[ESTADO] AS FORNECEDOR_ESTADO,
		          
	            FROM 
		            [TBMEDICAMENTO] AS MEDICAMENTO INNER JOIN
                    [TBFORNECEDOR] AS FORNECEDOR
                ON
                    MEDICAMENTO.[FORNECEDOR_NUMERO] = FORNECEDOR.[ID]";

        private const string sqlSelecionarPorNumero =
          @"SELECT 
		            MEDICAMENTO.[ID],
		            MEDICAMENTO.[NOME],
		            MEDICAMENTO.[DESCRICAO],
		            MEDICAMENTO.[LOTE],
		            MEDICAMENTO.[VALIDADE],
		            MEDICAMENTO.[QUANTIDADEDISPONIVEL],
                    
                    FORNECEDOR.[ID] AS FORNECEDOR_NUMERO,
                    FORNECEDOR.[NOME] AS FORNECEDOR_NOME,
                    FORNECEDOR.[TELEFONE] AS FORNECEDOR_TELEFONE,
                    FORNECEDOR.[EMAIL] AS FORNECEDOR_EMAIL,
                    FORNECEDOR.[CIDADE] AS FORNECEDOR_CIDADE,
                    FORNECEDOR.[ESTADO] AS FORNECEDOR_ESTADO,
		          
	            FROM 
		            [TBMEDICAMENTO] AS MEDICAMENTO INNER JOIN
                    [TBFORNECEDOR] AS FORNECEDOR
                ON
                    MEDICAMENTO.[FORNECEDOR_NUMERO] = FORNECEDOR.[ID]
                WHERE
                    MEDICAMENTO.[ID] = @ID";

        private const string sqlSelecionarRequisicao =
           @"SELECT                
                REQUISICAO.[ID],
                REQUISICAO.[QUANTIDADEMEDICAMENTO],
                REQUISICAO.[DATA],

                FUNCIONARIO.[ID] AS FUNCIONARIO_ID ,
                FUNCIONARIO.[NOME] AS FUNCIONARIO_NOME,
                FUNCIONARIO.[LOGIN] AS FUNCIONARIO_LOGIN,
                FUNCIONARIO.[SENHA] AS FUNCIONARIO_SENHA,

                PACIENTE.[ID] AS PACIENTE_ID,
                PACIENTE.[NOME] AS PACIENT_NOME,
                PACIENTE.[CARTAOSUS] AS PACIENTE_CARTAOSUS
               
            FROM
                TBREQUISICAO AS REQUISICAO INNER JOIN 
                TBPaciente AS PACIENTE 
            ON 
                REQUISICAO.[PACIENTE_ID] = PACIENTE.[ID]
                INNER JOIN TBFUNCIONARIO 
            ON
                REQUISICAO.[FUNCIONARIO_ID] = FUNCIONARIO.[ID]";

        private const string sqlSelecionarRequisicaoPorNumero =
           @"SELECT                
                REQUISICAO.[ID],
                REQUISICAO.[QUANTIDADEMEDICAMENTO],
                REQUISICAO.[DATA],

                FUNCIONARIO.[ID] AS FUNCIONARIO_ID ,
                FUNCIONARIO.[NOME] AS FUNCIONARIO_NOME,
                FUNCIONARIO.[LOGIN] AS FUNCIONARIO_LOGIN,
                FUNCIONARIO.[SENHA] AS FUNCIONARIO_SENHA,

                PACIENTE.[ID] AS PACIENTE_ID,
                PACIENTE.[NOME] AS PACIENT_NOME,
                PACIENTE.[CARTAOSUS] AS PACIENTE_CARTAOSUS
               
            FROM
                TBREQUISICAO AS REQUISICAO INNER JOIN 
                TBPaciente AS PACIENTE 
            ON 
                REQUISICAO.[PACIENTE_ID] = PACIENTE.[ID]
                INNER JOIN TBFUNCIONARIO 
            ON
                REQUISICAO.[FUNCIONARIO_ID] = FUNCIONARIO.[ID]
            WHERE
                REQUISICAO.[MEDICAMENTO_ID] = @ID";

        #endregion

        public ValidationResult Inserir(Medicamento novomedicamento)
        {
            var validador = new ValidadorMedicamentos();

            var resultadoValidacao = validador.Validate(novomedicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(novomedicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novomedicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamentos();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Medicamento SelecionarPorNumero(int numero)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecionarPorNumero = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);
            comandoSelecionarPorNumero.Parameters.AddWithValue("ID", numero);

            conexaoComBanco.Open();
            SqlDataReader sqlDataReader = comandoSelecionarPorNumero.ExecuteReader();

            Medicamento medicamento = null;

            if (sqlDataReader.Read())
            {
                medicamento = ConverterParaMedicamento(sqlDataReader);

                SqlCommand sqlComandoRequisicao = new SqlCommand
                    (sqlSelecionarRequisicaoPorNumero, conexaoComBanco);

                sqlComandoRequisicao.Parameters.AddWithValue("ID", medicamento.Id);

                SqlDataReader sqlDataReaderRequisicao = sqlComandoRequisicao.ExecuteReader();

                List<Requisicao> requisicoes = new List<Requisicao>();
                while (sqlDataReaderRequisicao.Read())
                {
                    Requisicao requisicao = null;
                    requisicao = ConverterRequisicao(sqlDataReaderRequisicao);
                    requisicao.Medicamento = medicamento;
                    requisicoes.Add(requisicao);
                }
                medicamento.Requisicoes = requisicoes;
            }
            conexaoComBanco.Close();

            return medicamento;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            SqlCommand comandoSelecionarTodos = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader sqlDataReader = comandoSelecionarTodos.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();
            List<Requisicao> todasRequisicoes = new List<Requisicao>();

            SqlCommand sqlCommandRequisicao = new SqlCommand
            (sqlSelecionarRequisicao, conexaoComBanco);

            SqlDataReader sqlDataReaderRequisicao = sqlCommandRequisicao.ExecuteReader();

            while (sqlDataReaderRequisicao.Read())
                todasRequisicoes.Add(ConverterRequisicao(sqlDataReaderRequisicao));

            todasRequisicoes.OrderBy(x => x.Medicamento.Id);

            int i = 0;
            while (sqlDataReader.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(sqlDataReader);

                List<Requisicao> requisicoes = new List<Requisicao>();

                //Requisicao requisicao = null;

                while (todasRequisicoes[i].Id == medicamento.Id)
                {
                    todasRequisicoes[i].Medicamento = medicamento;
                    requisicoes.Add(todasRequisicoes[i]);
                    i++;
                }

                medicamento.Requisicoes = requisicoes;
                medicamentos.Add(medicamento);
            }
            return medicamentos;
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            int numero = Convert.ToInt32(leitorMedicamento["ID"]);
            string nome = Convert.ToString(leitorMedicamento["NOME"]);
            string descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            string lote = Convert.ToString(leitorMedicamento["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);
            int fornecedorId = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);

            var medicamento = new Medicamento
                (nome, descricao, lote, validade)
            {
                Id = numero,
                QuantidadeDisponivel = quantidadeDisponivel
            };

            return medicamento;
        }

        private static void ConfigurarParametrosMedicamento(Medicamento novoMedicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoMedicamento.Id);
            comando.Parameters.AddWithValue("NOME", novoMedicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", novoMedicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", novoMedicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", novoMedicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", novoMedicamento.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", novoMedicamento.Fornecedor.Id);
            
        }
       
        public static void ConfigurarParametrosRequisicao(Requisicao requisicao, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", requisicao.Id);
            sqlCommand.Parameters.AddWithValue("FUNCIONARIO_ID", requisicao.Funcionario.Id);
            sqlCommand.Parameters.AddWithValue("PACIENTE_ID", requisicao.Paciente.Id);
            sqlCommand.Parameters.AddWithValue("MEDICAMENTO_ID", requisicao.Medicamento.Id);
            sqlCommand.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", requisicao.QtdMedicamento);
            sqlCommand.Parameters.AddWithValue("DATA", requisicao.Data);
        }

        public static Requisicao ConverterRequisicao(SqlDataReader leitorRequisicao)
        {
            int numero = Convert.ToInt32(leitorRequisicao["ID"]);

            int funcionarioID = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);

            string funcionarioNome = Convert.ToString(leitorRequisicao["FUNCIONARIO_NOME"]);
            string funcionarioLogin = Convert.ToString(leitorRequisicao["LOGIN"]);
            string funcionarioSenha = Convert.ToString(leitorRequisicao["SENHA"]);

            int pacienteID = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            string pacienteNome = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);
            string pacienteSUS = Convert.ToString(leitorRequisicao["PACIENTE_CARTAOSUS"]);

            DateTime data = Convert.ToDateTime(leitorRequisicao["DATA"]);
            int quantidadeMedicamento = Convert.ToInt32
                                            (leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
           
            var requisicao = new Requisicao
            {
                Id = numero,
                Funcionario = new Funcionario
                            (funcionarioNome, funcionarioLogin, funcionarioSenha)
                {
                    Id = funcionarioID
                },

                Paciente = new Paciente(pacienteNome, pacienteSUS)
                {
                    Id = pacienteID
                },
                QtdMedicamento = quantidadeMedicamento,
                Data = data
            };

            return requisicao;
        }
    }
}
