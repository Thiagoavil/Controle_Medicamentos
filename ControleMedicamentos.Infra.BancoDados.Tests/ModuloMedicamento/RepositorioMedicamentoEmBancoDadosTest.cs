using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        Medicamento medicamento;
        Fornecedor fornecedor;

        RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        RepositorioFornecedorEmBancoDados repositorioFornecedor;

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBREQUISICAO; DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBMEDICAMENTO; DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)");
            db.ComandoSql("DELETE FROM TBFORNECEDOR; DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)");
            
            fornecedor = new("umbrella", "322241578", " thiago@gmail.com", "Joiville", "SC")
            {

            };

            medicamento = new("Diclofenax","relaxante","LI4567",DateTime.Now )
            {
                Fornecedor=fornecedor
            };


            repositorioMedicamento = new();
            repositorioFornecedor = new();
        }
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);
           
            var medicamentoRegistrado = repositorioMedicamento.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoRegistrado);

            Assert.AreEqual(medicamento, medicamentoRegistrado);
        }

        [TestMethod]
        public void Deve_Editar_Medicamento()
        {
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            medicamento.Nome = "nimesulid";
            medicamento.Descricao = "Anti inflamatório";
            medicamento.Lote = "l87tra";
            medicamento.QuantidadeDisponivel = 5;

            repositorioMedicamento.Editar(medicamento);

            var medicamentoEditado = repositorioMedicamento.SelecionarPorNumero(medicamento.Id);


            Assert.IsNotNull(medicamentoEditado);

            Assert.AreEqual(medicamento, medicamentoEditado);
        }

        [TestMethod]
        public void Deve_Excluir_Medicamento()
        {
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            repositorioMedicamento.Excluir(medicamento);

            var MedicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(medicamento.Id);

            Assert.IsNull(MedicamentoEncontrado);

        }

        [TestMethod]
        public void Deve_Selecionar_Um_Medicamento()
        {
            repositorioFornecedor.Inserir(fornecedor);
            repositorioMedicamento.Inserir(medicamento);

            var MedicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(MedicamentoEncontrado);

            Assert.AreEqual(medicamento, MedicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Medicamento()
        {
            int quantidade = 3;
           

            for (int i = 0; i < quantidade; i++)
            {
                repositorioFornecedor.Inserir(fornecedor);
                repositorioMedicamento.Inserir(medicamento);
            }
                

            var medicamentos = repositorioMedicamento.SelecionarTodos();

            Assert.AreEqual(quantidade, medicamentos.Count);

        }

    }
}
