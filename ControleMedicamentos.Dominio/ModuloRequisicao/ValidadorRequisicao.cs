using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                .NotNull().WithMessage("Campo 'Medicamento' não pode ser nulo");

            RuleFor(x => x.Paciente)
                .NotNull().WithMessage("Campo 'Paciente' não pode ser nulo ");

            RuleFor(x => x.Funcionario)
                .NotNull().WithMessage("Campo 'Funcionario' não pode ser nulo");

            RuleFor(x => x.QtdMedicamento)
                .NotEmpty().WithMessage("Campo 'Quantidade de Medicamento' não pode ser vazia ");

            RuleFor(x => x.Data)
                .GreaterThan(System.DateTime.MinValue).WithMessage("Campo 'Data' incorreto");
        }
    }
}
