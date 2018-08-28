using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class LocalizacaoMap : ClassMap<Localizacao>
    {
        public LocalizacaoMap()
        {
            Id(x => x.LocalizacaoId).GeneratedBy.GuidComb();

            Map(x => x.HorarioAmostra);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            References(x => x.Rota, "RotaId").Cascade.None();
        }
    }
}