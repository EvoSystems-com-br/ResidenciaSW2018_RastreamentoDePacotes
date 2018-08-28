using AplicacaoArduino.App_Start;
using AplicacaoArduino.Models;
using Common.Models;
using Common.Services;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AplicacaoArduino.Controllers
{
    public class RastreamentoController : Controller
    {
        private static readonly JavaScriptSerializer serial = new JavaScriptSerializer();
        private static readonly string connString = ConfigurationManager.AppSettings.Get("DB");

        // GET: Rastreamento
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public string AdicionarLocalizacao([FromBody] AmostraLocalizacao amostra)
        {

            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {
                foreach(var tag in amostra.Tags)
                {
                    var pacote = _session.QueryOver<Pacote>().Where(x => x.TagRFID == tag).List().First();
                    var rota = pacote.Rotas.Last();

                    var local = new Localizacao
                    {
                        HorarioAmostra = DateTimeOffset.Now.LocalDateTime,
                        Latitude = amostra.Latitude,
                        Longitude = amostra.Longitude,
                        Rota = rota,
                    };

                    _session.SaveOrUpdate(local);
                    _session.Flush();
                }

                _session.Close();
            }

            return serial.Serialize(new RespostaHttp
            {
                Ok = true,
                Mensagem = "Rastreamento OK"
            });
        }

        [System.Web.Mvc.HttpPost]
        public string AdicionarPacote([FromBody] NovoPacote novoPacote)
        {
            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {
                Usuario user = _session.Get<Usuario>(novoPacote.UsuarioId);

                var addr = novoPacote.Endereco;

                _session.Save(addr);

                if(user != null)
                {
                    Pacote pacote = new Pacote
                    {
                        DataPostagem = DateTime.Now,
                        Destinatario = novoPacote.Destinatario,
                        Destino = addr,
                        Remetente = user,
                        Rotas = new List<Rota>(),
                        TagRFID = novoPacote.CodigoTag,
                        Entregue = false
                    };

                    _session.Save(pacote);
                }
                _session.Flush();
                _session.Close();
            }

            var resp =  new RespostaHttp
            {
                Ok = true,
                Mensagem = "Criação de Pacote OK"
            };

            return serial.Serialize(resp);
        }

        [System.Web.Mvc.HttpPost]
        public string CriarRota([FromBody] NovaRota novaRota)
        {

            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {
                var pacote = _session.Get<Pacote>(novaRota.PacoteId);
                var estacaoOrigem = _session.Get<Estacao>(novaRota.EstacaoOrigemId);
                var estacaoDestino = _session.Get<Estacao>(novaRota.EstacaoDestinoId);
                var veiculo = _session.Get<Veiculo>(novaRota.VeiculoId);

                Rota rota = new Rota
                {
                    AmostrasLocalizacao = new List<Localizacao>(),
                    DataInicio = DateTime.Now,
                    Origem = estacaoOrigem,
                    Destino = estacaoDestino,
                    Pacote = pacote,
                    VeiculoTransporte = veiculo
                };

                pacote.Rotas.Add(rota);

                _session.Update(pacote);
                _session.Save(rota);

                _session.Flush();
                _session.Close();
            }

            var resp = new RespostaHttp
            {
                Ok = true,
                Mensagem = "Criação de Rota OK"
            };

            return serial.Serialize(resp);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<string> CriarEstacao([FromBody] Endereco enderecoEstacao)
        {
            var coords = await GeocodingService.obterCoordenadas(enderecoEstacao);

            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                _session.Save(enderecoEstacao);

                Estacao estacao = new Estacao
                {
                    Endereco = enderecoEstacao,
                    Latitude = coords.Latitude,
                    Longitude = coords.Longitude,
                    PacotesAtuais = new List<Pacote>(),
                };
                _session.Save(estacao);
                _session.Flush();
                _session.Close();
            }

            var resp = new RespostaHttp
            {
                Ok = true,
                Mensagem = "Criação de Estacao OK"
            };

            return serial.Serialize(resp);
        }

        [System.Web.Mvc.HttpPost]
        public string CriarVeiculo([FromBody] string helloWorld)
        {

            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                Veiculo veiculo = new Veiculo
                {
                    PacotesAtuais = new List<Pacote>(),
                    RotaAtual = null
                };

                _session.Save(veiculo);

                _session.Flush();
                _session.Close();
            }

            var resp = new RespostaHttp
            {
                Ok = true,
                Mensagem = "Criação de Veículo OK"
            };

            return serial.Serialize(resp);
        }

        [System.Web.Mvc.HttpGet]
        public string ObterEstacoes()
        {
            ISession _session = NHibernateFactory.CreateSession(connString).OpenSession();
            var serializer = new JavaScriptSerializer();
            var estacoes = RespostaEstacao.CopiarDeLista(_session.QueryOver<Estacao>().List());

            var resp = new RespostaHttp
            {
                Ok = true,
                Mensagem = serializer.Serialize(estacoes)
            };

            _session.Flush();
            _session.Close();

            return serial.Serialize(resp);
        }

        [System.Web.Mvc.HttpGet]
        public string ObterPacotes()
        {
            ISession _session = NHibernateFactory.CreateSession(connString).OpenSession();
            var serializer = new JavaScriptSerializer();
            var pacs = _session.QueryOver<Pacote>().List();

            var pacotes = new Collection<RespostaPacote>();

            foreach(var item in pacs)
            {
                pacotes.Add(new RespostaPacote(item));
            }

            var resp = new RespostaHttp
            {
                Ok = true,
                Mensagem = serializer.Serialize(pacotes)
            };

            _session.Flush();
            _session.Close();

            return serial.Serialize(resp);
        }

    }
}