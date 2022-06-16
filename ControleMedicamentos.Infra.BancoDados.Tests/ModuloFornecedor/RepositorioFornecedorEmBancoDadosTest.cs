using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public  class RepositorioFornecedorEmBancoDadosTest
    {
        Fornecedor fornecedor;

        RepositorioFornecedorEmBancoDados repositorioFornecedor;

        public RepositorioFornecedorEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");

            fornecedor = new("Fornecedor1", "987654321", " Fornecedor@gmail.com", "Lages", "SC");

            repositorioFornecedor = new();
        }


        [TestMethod]
        public void Deve_Inserir_Fornecedor()
        {
            repositorioFornecedor.Inserir(fornecedor);

            var fornecedorRegistrado = repositorioFornecedor.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(fornecedorRegistrado);

            Assert.AreEqual(fornecedor, fornecedorRegistrado);
        }

        [TestMethod]
        public void Deve_Editar_Fornecedor()
        {
            repositorioFornecedor.Inserir(fornecedor);

            fornecedor.Nome = "Tio Home";
            fornecedor.Telefone = "456132789";
            fornecedor.Email = "TioHome@gmail.com";
            fornecedor.Cidade = "Curitiba";
            fornecedor.Estado = "PR";

            repositorioFornecedor.Editar(fornecedor);

            var fornecedorEditado = repositorioFornecedor.SelecionarPorNumero(fornecedor.Id);


            Assert.IsNotNull(fornecedorEditado);

            Assert.AreEqual(fornecedor, fornecedorEditado);
        }


        [TestMethod]
        public void Deve_Excluir_Fornecedor()
        {

            repositorioFornecedor.Inserir(fornecedor);

            repositorioFornecedor.Excluir(fornecedor);

            var fornecedorEncontrado = repositorioFornecedor.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Funcionarioe()
        {
            repositorioFornecedor.Inserir(fornecedor);

            var FornecedorEncontrado = repositorioFornecedor.SelecionarPorNumero(fornecedor.Id);

            Assert.IsNotNull(FornecedorEncontrado);

            Assert.AreEqual(fornecedor, FornecedorEncontrado);
        }


        [TestMethod]
        public void Deve_Selecionar_Todos_Fornecedor()
        {
            int quantidade = 3;

            for (int i = 0; i < quantidade; i++)
                repositorioFornecedor.Inserir(fornecedor);

            var fornecedores = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(quantidade, fornecedores.Count);

        }
    }
}
