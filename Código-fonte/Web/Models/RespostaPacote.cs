using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class RespostaPacote
    {
        public Guid PacoteId { get; set; }
        public virtual string TagRFID { get; set; }
        public virtual RespostaUsuario Remetente { get; set; }
        public virtual string Destinatario { get; set; }
        public virtual ICollection<RespostaRota> Rotas { get; set; }
        public virtual DateTime DataPostagem { get; set; }
        public virtual Endereco Destino { get; set; }
        public virtual bool Entregue { get; set; }

        public RespostaPacote(Pacote pac)
        {
            PacoteId = pac.PacoteId;
            TagRFID = pac.TagRFID;
            Remetente = new RespostaUsuario(pac.Remetente);
            Destinatario = pac.Destinatario;
            DataPostagem = pac.DataPostagem;
            Destino = pac.Destino;
            Entregue = pac.Entregue;

            Rotas = new Collection<RespostaRota>();

            foreach(var item in pac.Rotas)
            {
                Rotas.Add(new RespostaRota(item));
            }
        }
    }
}