using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.Exceptions
{
    /*
     * Exceçãoa personalizada de serviço para erros de integridade referencial, 
     * ou seja quando se quer deletar entidades que possuem dependência com outras
     * Exemplo vendedor e vendas.
     */
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {
        }
    }
}
