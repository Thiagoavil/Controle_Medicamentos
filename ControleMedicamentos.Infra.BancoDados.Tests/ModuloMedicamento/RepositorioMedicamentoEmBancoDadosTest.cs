using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        Medicamento medicamento;

        RepositorioMedicamentoEmBancoDados repositorioMedicamento;

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            db.ComandoSql("DELETE FROM TBPACIENTE; DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)");

            medicamento = new("Diclofenax","relaxante","LI4567",DateTime.Now )
            {
                Fornecedor=fornecedor
            };


            repositorioMedicamento = new();
        }
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            repositorioMedicamento.Inserir(medicamento);

            var medicamentoRegistrado = repositorioMedicamento.SelecionarPorNumero(medicamento.Id);

            Assert.IsNotNull(medicamentoRegistrado);

            Assert.AreEqual(medicamento, medicamentoRegistrado);
        }

        Fornecedor fornecedor = new ("umbrella", "322241578", " thiago@gmail.com", "Joiville", "SC")
        {
            Id = 1,
        };
    }
}
