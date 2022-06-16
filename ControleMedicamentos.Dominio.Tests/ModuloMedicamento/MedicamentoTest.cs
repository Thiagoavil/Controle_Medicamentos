using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void Nome_nao_Pode_Ser_Nulo()
        {
            Medicamento medicamento = new(null, "Descrição1", "Lote1", DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_nao_Pode_Ser_Vazio()
        {
            Medicamento medicamento = new("", "Descrição1", "Lote1", DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_nao_Pode_Ser_Nulo()
        {
            Medicamento medicamento = new("Medicamento1", null, "Lote1", DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Descricao' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_nao_Pode_Ser_Vazio()
        {
            Medicamento medicamento = new("Medicamento1", "", "Lote1", DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Descricao' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_nao_Pode_Ser_Nulo()
        {
            Medicamento medicamento = new("Medicamento1", "Descrição1", null, DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Lote' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_nao_Pode_Ser_Vazio()
        {
            Medicamento medicamento = new("Medicamento1", "Descrição1", "", DateTime.Now);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Lote' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Validade_Incorreta()
        {
            Medicamento medicamento = new("Medicamento1", "Descrição1", "Lote1", DateTime.MinValue);

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Validade' incorreto", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Fornecedor_nao_Pode_Ser_Nulo()
        {
            Medicamento medicamento = new("Medicamento1", "Descrição1", "Lote1", DateTime.Now);
            medicamento.QuantidadeDisponivel = 15;
            medicamento.Fornecedor = null;

            ValidadorMedicamentos validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(medicamento);

            //assert
            Assert.AreEqual("Campo 'Fornecedor' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }
    }
}
