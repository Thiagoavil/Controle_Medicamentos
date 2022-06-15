using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    internal class RepositorioRequisicaoEmBancoDados
    {
        private const string enderecoBanco =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog ControleMedicamentos.Projeto.SqlServer;" +
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
                    MEDICAMENTO.[QUANTIDADEDISPONIVEL] AS MEDICAMENTO_QUANTIDADE
		          
	            FROM 
		            [TBREQUISICAO] AS REQUISICAO INNER JOIN
                    [TBFUNCIONARIO] AS FORNECEDOR
                ON
                    REQUISICAO.[FUNCIONARIO_NUMERO] = FUNCIONARIO.[ID]
                    INNER JOIN [TBPACIENTE] AS PACIENTE
                ON
                    REQUISICAO.[PACIENTE_NUMERO] = PACIENTE.[ID]
                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                ON
                    REQUISICAO.[MEDICAMENTO_NUMERO] = MEDICAMENTO.[ID]";

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
                    MEDICAMENTO.[QUANTIDADEDISPONIVEL] AS MEDICAMENTO_QUANTIDADE
		          
	            FROM 
		            [TBREQUISICAO] AS REQUISICAO INNER JOIN
                    [TBFUNCIONARIO] AS FORNECEDOR
                ON
                    REQUISICAO.[FUNCIONARIO_NUMERO] = FUNCIONARIO.[ID]
                    INNER JOIN [TBPACIENTE] AS PACIENTE
                ON
                    REQUISICAO.[PACIENTE_NUMERO] = PACIENTE.[ID]
                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                ON
                    REQUISICAO.[MEDICAMENTO_NUMERO] = MEDICAMENTO.[ID]
                WHERE
                    REQUISICAO.[ID] = @ID";
        
        #endregion

    }
}
