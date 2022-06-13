using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
        public int QuantidadeDisponivel { get; set; }

        public List<Requisicao> Requisicoes { get; set; }

        public Fornecedor Fornecedor{ get; set; }

        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public Medicamento(string nome, string descricao, string lote, DateTime validade)
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            Requisicoes = new List<Requisicao>();
        }

        public override bool Equals(object obj)
        {
            Medicamento medicamento = obj as Medicamento;

            if (medicamento == null)
                return false;

            return
                medicamento.Id.Equals(Id) &&
                medicamento.Nome.Equals(Nome) &&
                medicamento.Descricao.Equals(Descricao) &&
                medicamento.Lote.Equals(Lote) &&
                medicamento.Validade.Equals(Validade) &&
                medicamento.QuantidadeDisponivel.Equals(QuantidadeDisponivel) &&
                medicamento.Requisicoes.Equals(Requisicoes) &&
                medicamento.Fornecedor.Equals(Fornecedor) &&
                medicamento.QuantidadeRequisicoes.Equals(QuantidadeRequisicoes);
        }
    }
}
