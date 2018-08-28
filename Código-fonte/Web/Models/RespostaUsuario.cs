using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoArduino.Models
{
    public class RespostaUsuario
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public RespostaUsuario(Usuario user)
        {
            UsuarioId = user.UsuarioId;
            Nome = user.Nome;
            Email = user.Email;
        }
    }
}