using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class NovoPacote
    {
        public Guid UsuarioId { get; set; }
        public Endereco Endereco { get; set; }
        public string Destinatario { get; set; }
        public string CodigoTag { get; set; }
    }
}