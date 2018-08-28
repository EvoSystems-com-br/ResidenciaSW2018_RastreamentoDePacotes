using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class EstacaoMap : ClassMap<Estacao>
    {
        public EstacaoMap()
        {
            Id(x => x.EstacaoId).GeneratedBy.GuidComb();

            Map(x => x.Latitude);
            Map(x => x.Longitude);
            HasManyToMany(x => x.PacotesAtuais).Cascade.None();
            References(x => x.Endereco, "EnderecoId").Cascade.None();
        }
    }
}