using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        Requisicao requisicao;

        RepositorioRequisicaoEmBancoDados repositorioRequisicao;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");

            requisicao = new()
            {
                Funcionario=funcionario,
                Paciente=paciente,
                QtdMedicamento = 5,
                Data = DateTime.Now
            };

            repositorioRequisicao = new();
        }

        [TestMethod]
        public void Deve_Inserir_Requisição()
        {
            repositorioRequisicao.Inserir(requisicao);

            var fornecedorRegistrado = repositorioRequisicao.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(fornecedorRegistrado);

            Assert.AreEqual(requisicao, fornecedorRegistrado);
        }
        
        
        
        
        
        
        
        Funcionario funcionario = new("thiago", "login", " password")
        {
            Id = 1,
        };
        Paciente paciente = new("Julia Roberts", "98754124")
        {
            Id = 1,
        };
    }
}
