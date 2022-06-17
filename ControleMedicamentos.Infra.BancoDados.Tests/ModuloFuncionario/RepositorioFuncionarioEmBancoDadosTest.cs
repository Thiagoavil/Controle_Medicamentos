using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        Funcionario funcionario;

        RepositorioFuncionarioEmBancoDados repositorioFuncionario;

        public RepositorioFuncionarioEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBFUNCIONARIO; DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)");

            funcionario = new("thiago", "login", " password");

            repositorioFuncionario = new();


        }

        [TestMethod]
        public void Deve_Inserir_Funcionario()
        {
            repositorioFuncionario.Inserir(funcionario);

            var funcionarioRegistrado = repositorioFuncionario.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(funcionarioRegistrado);

            Assert.AreEqual(funcionario, funcionarioRegistrado);
        }

        [TestMethod]
        public void Deve_Editar_Funcionario()
        {
            repositorioFuncionario.Inserir(funcionario);

            funcionario.Nome = "thiaguinho";
            funcionario.Login = "login123";
            funcionario.Senha = "answer";


            repositorioFuncionario.Editar(funcionario);

            var funcionarioRegistrado = repositorioFuncionario.SelecionarPorNumero(funcionario.Id);


            Assert.IsNotNull(funcionarioRegistrado);

            Assert.AreEqual(funcionario, funcionarioRegistrado);
        }

        [TestMethod]
        public void Deve_Excluir_Funcionarior()
        {

            repositorioFuncionario.Inserir(funcionario);

            repositorioFuncionario.Excluir(funcionario);

            var funcionarioEncontrado = repositorioFuncionario.SelecionarPorNumero(funcionario.Id);

            Assert.IsNull(funcionarioEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Funcionarior()
        {
            repositorioFuncionario.Inserir(funcionario);

            var FuncionarioEncontrado = repositorioFuncionario.SelecionarPorNumero(funcionario.Id);

            Assert.IsNotNull(FuncionarioEncontrado);

            Assert.AreEqual(funcionario, FuncionarioEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Funcionarior()
        {
            int quantidade = 3;

            for (int i = 0; i < quantidade; i++)
                repositorioFuncionario.Inserir(funcionario);

            var funcionarios = repositorioFuncionario.SelecionarTodos();

            Assert.AreEqual(quantidade, funcionarios.Count);

        }
    }
}
