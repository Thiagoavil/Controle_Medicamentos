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
            Fornecedor fornecedor = new(null, "49587621", "ttaa@gmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_nao_Pode_Ser_Vazio()
        {
            Fornecedor fornecedor = new("", "49587621", "ttaa@gmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_nao_Pode_Ser_Nulo()
        {
            Fornecedor fornecedor = new("thiago", null, "ttaa@gmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Telefone' Não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_nao_Pode_Ser_Vazio()
        {
            Fornecedor fornecedor = new("thiago", "", "ttaa@gmail.com", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Telefone' Não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Email_nao_Pode_Ser_Nulo()
        {
            Fornecedor fornecedor = new("thiago", "49587621", null, "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Email' Não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_nao_Pode_Ser_Vazio()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "", "Joinville", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Email' Não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }



        [TestMethod]
        public void Email_Formato()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "ttt.gmail ", "Joinville", "SC");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Email' Formato incorreto", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_nao_Pode_Ser_Nulo()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "ttaa@gmail.com", null, "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Cidade' Não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_nao_Pode_Ser_Vazio()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "ttaa@gmail.com", "", "SC");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Cidade' Não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Estado_nao_Pode_Ser_Nulo()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "ttaa@gmail.com", "Joinville", null);

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Estado' Não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_nao_Pode_Ser_Vazio()
        {
            Fornecedor fornecedor = new("thiago", "49587621", "ttaa@gmail.com", "Joinville", "");

            ValidadorFornecedor validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(fornecedor);

            //assert
            Assert.AreEqual("Campo 'Estado' Não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

    }
}
