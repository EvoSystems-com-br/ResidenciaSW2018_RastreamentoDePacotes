using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class NovaRota
    {
        public Guid PacoteId { get; set; }
        public Guid EstacaoOrigemId { get; set; }
        public Guid EstacaoDestinoId { get; set; }
        public Guid VeiculoId { get; set; }
    }
}