using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class FuncionarioTest
    {
        [TestMethod]
        public void Nome_nao_Pode_Ser_Nulo()
        {
            Funcionario funcionario = new(null, "Login1", "Senha1");

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_nao_Pode_Ser_Vazio()
        {
            Funcionario funcionario = new("", "Login1", "Senha1");

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_nao_Pode_Ser_Nulo()
        {
            Funcionario funcionario = new("Funcionario1", null, "Senha1");

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Login' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_nao_Pode_Ser_Vazio()
        {
            Funcionario funcionario = new("Funcionario1", "", "Senha1");

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Login' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_nao_Pode_Ser_Nulo()
        {
            Funcionario funcionario = new("Funcionario1", "Login1", null);

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Senha' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_nao_Pode_Ser_Vazio()
        {
            Funcionario funcionario = new("Funcionario1", "Login1", "");

            ValidadorFuncionario validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(funcionario);

            //assert
            Assert.AreEqual("Campo 'Senha' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }
    }
}
