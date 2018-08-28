using Common.Models;
using FluentNHibernate.Mapping;

namespace AplicacaoArduino.Mappings
{
    public class PacoteMap : ClassMap<Pacote>
    {
        public PacoteMap()
        {
            Id(x => x.PacoteId).GeneratedBy.GuidComb();

            Map(x => x.TagRFID);
            Map(x => x.DataPostagem);
            Map(x => x.Entregue);
            Map(x => x.Destinatario);
            HasMany(x => x.Rotas).KeyColumn("PacoteId").Cascade.None();
            References(x => x.Destino, "EnderecoId").Cascade.None();
            References(x => x.Remetente, "UsuarioId").Cascade.None();
        }
    }
}