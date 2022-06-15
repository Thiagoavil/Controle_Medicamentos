using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'Nome' Não pode ser vazio");

            RuleFor(x => x.Telefone)
                .NotNull().WithMessage("Campo 'Telefone' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'Telefone' Não pode ser vazio");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Campo 'Email' Formato incorreto");

            RuleFor(x => x.Cidade)
                .NotNull().WithMessage("Campo 'Cidade' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'Cidade' Não pode ser vazio");

            RuleFor(x => x.Estado)
               .NotNull().WithMessage("Campo 'Estado' Não pode ser nulo")
                .NotEmpty().WithMessage("Campo 'Estado' Não pode ser vazio");
        }
    }
}

