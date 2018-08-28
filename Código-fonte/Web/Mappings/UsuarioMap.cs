using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Id(x => x.UsuarioId).GeneratedBy.GuidComb();
            Map(x => x.Email);
            Map(x => x.Nome);
            Map(x => x.Senha);
            Map(x => x.DataCadastro);
            HasManyToMany(x => x.PacotesRecebidos).Cascade.None();
            HasMany(x => x.PacotesEnviados).KeyColumn("UsuarioId").Cascade.None();

        }
    }
}