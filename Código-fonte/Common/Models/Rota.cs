using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Rota
    {
        public virtual Guid RotaId { get; set; }
        public virtual Pacote Pacote { get; set; }
        public virtual Veiculo VeiculoTransporte { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual Estacao Origem { get; set; }
        public virtual Estacao Destino { get; set; }
        public virtual ICollection<Localizacao> AmostrasLocalizacao { get; set; }
    }
}
