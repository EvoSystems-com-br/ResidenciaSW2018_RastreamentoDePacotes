using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class RespostaEstacao
    {
        public virtual Guid EstacaoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        public RespostaEstacao(Estacao estacao)
        {
            EstacaoId = estacao.EstacaoId;
            Endereco = estacao.Endereco;
            Latitude = estacao.Latitude;
            Longitude = estacao.Longitude;
        }

        public static IEnumerable<RespostaEstacao> CopiarDeLista(IEnumerable<Estacao> estacoes)
        {
            var resp = new Collection<RespostaEstacao>();

            foreach(var item in estacoes)
            {
                resp.Add(new RespostaEstacao(item));
            }

            return resp;
        }
    }
}