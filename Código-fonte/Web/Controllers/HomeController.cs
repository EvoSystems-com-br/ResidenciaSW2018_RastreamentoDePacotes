using AplicacaoArduino.App_Start;
using AplicacaoArduino.Models;
using AplicacaoArduino.ViewModels;
using Common.Models;
using MySql.Data.Types;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacaoArduino.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public Guid CreateUser()
        {
            string connString = ConfigurationManager.AppSettings.Get("DB");

            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                var user = new Usuario
                {
                    DataCadastro = DateTime.Now,
                    Email = "gustavo.shuiti.ferreira@usp.br",
                    Nome = "Gustavo",
                    Senha = "senha123",
                    PacotesEnviados = new List<Pacote>(),
                    PacotesRecebidos = new List<Pacote>()
                };
                _session.SaveOrUpdate(user);
                _session.Flush();

                return user.UsuarioId;
            }
        }

        public ActionResult Pacotes()
        {
            string connString = ConfigurationManager.AppSettings.Get("DB");
            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                var pacs = _session.QueryOver<Pacote>().OrderBy(x => x.DataPostagem).Asc.List();

                var pacots = new List<RespostaPacote>();

                foreach (var item in pacs)
                {
                    pacots.Add(new RespostaPacote(item));
                }

                var container = new ListContainerPacote
                {
                    pacotes = pacots
                };

                return View(container);
            }
        }

        public ActionResult Rotas(Guid PacoteId)
        {
            string connString = ConfigurationManager.AppSettings.Get("DB");
            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                var pacs = _session.QueryOver<Rota>().Where(x => x.Pacote.PacoteId == PacoteId).OrderBy(x => x.DataInicio).Asc.List();

                var pacots = new List<RespostaRota>();

                foreach (var item in pacs)
                {
                    pacots.Add(new RespostaRota(item));
                }

                var container = new ListContainerRota
                {
                    rotas = pacots
                };

                return View(container);
            }
        }

        public ActionResult Localizacoes(Guid RotaId)
        {
            string connString = ConfigurationManager.AppSettings.Get("DB");
            using (ISession _session = NHibernateFactory.CreateSession(connString).OpenSession())
            {

                var pacs = _session.QueryOver<Localizacao>().Where(x => x.Rota.RotaId == RotaId).OrderBy(x => x.HorarioAmostra).Asc.List();

                var pacots = new List<RespostaLocalizacao>();

                foreach (var item in pacs)
                {
                    pacots.Add(new RespostaLocalizacao(item));
                }

                var container = new ListContainerLocalizacao
                {
                    localizacoes = pacots
                };

                return View(container);
            }
        }

        public ActionResult Cugnasca()
        {
            return View();
        }
    }
}
