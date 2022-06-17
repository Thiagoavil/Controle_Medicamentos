using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
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
        Funcionario funcionario;
        Paciente paciente;
        Fornecedor fornecedor;
        Medicamento medicamento;

        RepositorioRequisicaoEmBancoDados repositorioRequisicao;
        RepositorioFuncionarioEmBancoDados repositorioFuncionario;
        RepositorioPacienteEmBancoDados repositorioPaciente;
        RepositorioFornecedorEmBancoDados repositorioFornecedor;
        RepositorioMedicamentoEmBancoDados repositorioMedicamento;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");

            funcionario = new("thiago", "login", " password");

            paciente = new("Julia Roberts", "98754124");

            fornecedor = new("umbrella", "322241578", " thiago@gmail.com", "Joiville", "SC");

            medicamento = new("buscopan", "relaxante", "ly567", DateTime.Now)
            {
                Fornecedor = fornecedor
            };
            
            requisicao = new()
            {
                Medicamento = medicamento,
                Funcionario= funcionario,
                Paciente= paciente,
                QtdMedicamento = 5,
                Data = DateTime.Now
            };

            repositorioRequisicao = new();
            repositorioFornecedor=new();
            repositorioMedicamento = new();
            repositorioFuncionario = new();
            repositorioPaciente=new();
        }

        [TestMethod]
        public void Deve_Inserir_Requisição()
        {
            repositorioPaciente.Inserir(paciente);
            repositorioFuncionario.Inserir(funcionario);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            var requisicaoRegistrada = repositorioRequisicao.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(requisicaoRegistrada);

            Assert.AreEqual(requisicao, requisicaoRegistrada);
        }

        [TestMethod]
        public void Deve_Editar_Requisicao()
        {
            repositorioPaciente.Inserir(paciente);
            repositorioFuncionario.Inserir(funcionario);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            requisicao.QtdMedicamento = 3;

            repositorioRequisicao.Editar(requisicao);

            var requisicaoditada = repositorioRequisicao.SelecionarPorNumero(requisicao.Id);


            Assert.IsNotNull(requisicaoditada);

            Assert.AreEqual(requisicao, requisicaoditada);
        }


        [TestMethod]
        public void Deve_Excluir_Requisicao()
        {
            repositorioPaciente.Inserir(paciente);
            repositorioFuncionario.Inserir(funcionario);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            repositorioRequisicao.Excluir(requisicao);

            var RequisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(requisicao.Id);

            Assert.IsNull(RequisicaoEncontrada);

        }

        [TestMethod]
        public void Deve_Selecionar_Uma_Requisicao()
        {
            repositorioPaciente.Inserir(paciente);
            repositorioFuncionario.Inserir(funcionario);
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
            repositorioRequisicao.Inserir(requisicao);

            var RequisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(requisicao.Id);

            Assert.IsNotNull(RequisicaoEncontrada);

            Assert.AreEqual(requisicao, RequisicaoEncontrada);
        }

        [TestMethod]
        public void Deve_Selecionar_Todas_as_Requisicoes()
        {
            int quantidade = 3;


            for (int i = 0; i < quantidade; i++)
            {
                repositorioPaciente.Inserir(paciente);
                repositorioFuncionario.Inserir(funcionario);
                repositorioFornecedor.Inserir(fornecedor);
                repositorioMedicamento.Inserir(medicamento);
                repositorioRequisicao.Inserir(requisicao);
            }


            var requisicoes = repositorioRequisicao.SelecionarTodos();

            Assert.AreEqual(quantidade, requisicoes.Count);

        }








    }
}
