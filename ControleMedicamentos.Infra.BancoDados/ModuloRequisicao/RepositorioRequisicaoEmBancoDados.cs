using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    internal class RepositorioRequisicaoEmBancoDados
    {
        private const string enderecoBanco =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog ControleMedicamentos.Projeto.SqlServer;" +
            "Integrated Security = True;" +
            "Pooling=False";
    }
}
