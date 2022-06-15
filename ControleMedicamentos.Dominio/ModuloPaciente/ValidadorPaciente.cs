using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'Nome' Não pode ser vazio");

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("Campo 'CartaoSUS' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'CartaoSUS' Não pode ser vazio");
        }
    }
}
