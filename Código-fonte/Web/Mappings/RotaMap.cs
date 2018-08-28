using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class RotaMap : ClassMap<Rota>
    {
        public RotaMap()
        {
            Id(x => x.RotaId).GeneratedBy.GuidComb();
            Map(x => x.DataInicio);
            Map(x => x.DataFim);
            References(x => x.VeiculoTransporte, "VeiculoId").Cascade.None();
            References(x => x.Origem, "EstacaoOrigemId").Cascade.None();
            References(x => x.Destino, "EstacaoDestinoId").Cascade.None();
            References(x => x.Pacote, "PacoteId").Cascade.None();
            HasMany(x => x.AmostrasLocalizacao).KeyColumn("RotaId").Cascade.None();
        }
    }
}