using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {   

        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }
        public int QtdMedicamento { get; set; }
        public DateTime Data { get; set; }
        public Funcionario Funcionario { get; set; }

        public void InserirPaciente(Paciente paciente)
        {
            if (paciente == null)
                return;

            Paciente = paciente;
        }

        public void InserirFuncionario(Funcionario funcionario)
        {
            if (funcionario == null)
                return;

            Funcionario = funcionario;
        }

        public void InserirMedicamento(Medicamento medicamento)
        {
            if (medicamento == null)
                return;

            Medicamento = medicamento;
        }
        public override bool Equals(object obj)
        {
            Requisicao requisicao = obj as Requisicao;

            if (requisicao == null)
                return false;

            return
                requisicao.Id.Equals(Id) &&
                requisicao.Medicamento.Equals(Medicamento) &&
                requisicao.Paciente.Equals(Paciente) &&
                requisicao.QtdMedicamento.Equals(QtdMedicamento) &&
                requisicao.Data.Equals(Data) &&
                requisicao.Funcionario.Equals(Funcionario);
        }
    }
}
