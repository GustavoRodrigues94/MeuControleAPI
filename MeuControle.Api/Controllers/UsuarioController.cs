using System.Threading.Tasks;
using MeuControle.Api.Services;
using MeuControle.Dominio.Comandos.UsuarioComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/Usuario")]
    public class UsuarioController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public GenericoComandoResultado Criar(
            [FromBody] CriarUsuarioComando comando,
            [FromServices] UsuarioManipulador manipulador) 
            => (GenericoComandoResultado)manipulador.Manipular(comando);

        [Route("login")]
        [HttpPost]
        public ActionResult<dynamic> Autenticar(
            [FromBody] AutenticarUsuarioComando comando,
            [FromServices] UsuarioManipulador manipulador)
        {
            var usuarioExistente = (GenericoComandoResultado)manipulador.Manipular(comando);

            if (!usuarioExistente.Sucesso)
                return NotFound(new { message = "Usuário ou senha inválidos." });

            var usuario = usuarioExistente.Dado as Usuario;

            var token = TokenService.GerarToken(usuario);

            usuario?.EsconderSenha();

            return new
            {
                Usuario = usuario,
                Token = token
            };
        }
    }
}
