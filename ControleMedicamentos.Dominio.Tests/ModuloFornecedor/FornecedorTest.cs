using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {
        [TestMethod]
        public void Nome_nao_Pode_Ser_Nulo()
        {
            //arrange
            Fornecedor fornecedor = new(null,"32225748","thiaguinhoPlay@hotmail.com","Joinville","SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser nulo", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Nome_nao_Pode_Ser_Vazio()
        {
            //arrange
            Fornecedor fornecedor = new("", "32225748", "thiaguinhoPlay@hotmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser vazio", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Telefone_nao_Pode_Ser_Nulo()
        {
            //arrange
            Fornecedor fornecedor = new("Thiago", null, "thiaguinhoPlay@hotmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Telefone' Não pode ser nulo", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Telefone_nao_Pode_Ser_Vazio()
        {
            //arrange
            Fornecedor fornecedor = new("Thiago", "" , "thiaguinhoPlay@hotmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Telefone' Não pode ser vazio", resultado.Errors[0].ErrorMessage);

        }
    }
}
