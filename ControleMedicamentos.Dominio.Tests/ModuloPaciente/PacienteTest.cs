using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class PacienteTest
    {
        [TestMethod]
        public void Nome_nao_Pode_Ser_Nulo()
        {
            //arrange
            Paciente paciente = new(null, "48579682");

            ValidadorPaciente validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(paciente);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser nulo", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Nome_nao_Pode_Ser_Vazio()
        {
            //arrange
            Paciente paciente = new("", "48579682");

            ValidadorPaciente validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(paciente);

            //assert
            Assert.AreEqual("Campo 'Nome' Não pode ser vazio", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Cartao_nao_Pode_Ser_Nulo()
        {
            //arrange
            Paciente paciente = new("José", null);

            ValidadorPaciente validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(paciente);

            //assert
            Assert.AreEqual("Campo 'CartaoSUS' Não pode ser nulo", resultado.Errors[0].ErrorMessage);

        }

        [TestMethod]
        public void Cartao_nao_Pode_Ser_Vazio()
        {
            //arrange
            Paciente paciente = new("José", "");

            ValidadorPaciente validacao = new();

            //action
            ValidationResult resultado = validacao.Validate(paciente);

            //assert
            Assert.AreEqual("Campo 'CartaoSUS' Não pode ser vazio", resultado.Errors[0].ErrorMessage);

        }
    }
}
