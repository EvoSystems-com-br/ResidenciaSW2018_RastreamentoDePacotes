using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class RespostaLocalizacao
    {
        public virtual Guid LocalizacaoId { get; set; }
        public virtual DateTime HorarioAmostra { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        public RespostaLocalizacao(Localizacao local)
        {
            LocalizacaoId = local.LocalizacaoId;
            HorarioAmostra = local.HorarioAmostra;
            Latitude = local.Latitude;
            Longitude = local.Longitude;
        }
    }
}