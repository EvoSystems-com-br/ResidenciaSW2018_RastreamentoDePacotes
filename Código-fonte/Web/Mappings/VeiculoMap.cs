using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class VeiculoMap : ClassMap<Veiculo>
    {
        public VeiculoMap()
        {
            Id(x => x.VeiculoId).GeneratedBy.GuidComb();

            References(x => x.RotaAtual, "RotaId").Cascade.None();
            HasManyToMany(x => x.PacotesAtuais).Cascade.None();

        }
    }
}