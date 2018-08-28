﻿using Common.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Mappings
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.EnderecoId).GeneratedBy.GuidComb();

            Map(x => x.Logradouro);
            Map(x => x.Numero);
            Map(x => x.Complemento);
            Map(x => x.Bairro);
            Map(x => x.Municipio);
            Map(x => x.Estado);
            Map(x => x.Pais);

        }
    }
}