using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class RespostaRota
    {
        public virtual Guid RotaId { get; set; }
        public virtual Veiculo VeiculoTransporte { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual ICollection<RespostaLocalizacao> AmostrasLocalizacao { get; set; }

        public RespostaRota(Rota rota)
        {
            RotaId = rota.RotaId;
            VeiculoTransporte = rota.VeiculoTransporte;
            DataInicio = rota.DataInicio;
            DataFim = rota.DataFim;

            AmostrasLocalizacao = new Collection<RespostaLocalizacao>();

            foreach(var item in rota.AmostrasLocalizacao)
            {
                AmostrasLocalizacao.Add(new RespostaLocalizacao(item));
            }
        }
    }
}