using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class AmostraLocalizacao
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime HorarioAmostra { get; set; }
        public List<string> Tags { get; set; }
    }
}