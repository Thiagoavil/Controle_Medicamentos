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

        public override bool Equals(object obj)
        {
            Requisicao medicamento = obj as Requisicao;

            if (medicamento == null)
                return false;

            return
                medicamento.Id.Equals(Id) &&
                medicamento.Medicamento.Equals(Medicamento) &&
                medicamento.Paciente.Equals(Paciente) &&
                medicamento.QtdMedicamento.Equals(QtdMedicamento) &&
                medicamento.Data.Equals(Data) &&
                medicamento.Funcionario.Equals(Funcionario);
        }
    }
}
