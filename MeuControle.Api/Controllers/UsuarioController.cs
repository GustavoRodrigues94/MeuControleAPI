using System.Threading.Tasks;
using MeuControle.Api.Services;
using MeuControle.Dominio.Comandos.UsuarioComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores;
using Microsoft.AspNetCore.Mvc;

namespace MeuControle.Api.Controllers
{
    [ApiController]
    [Route("v1/Usuario")]
    public class UsuarioController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<GenericoComandoResultado>> Criar(
            [FromBody] CriarUsuarioComando comando,
            [FromServices] UsuarioManipulador manipulador) 
            => await manipulador.Manipular(comando);

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromBody] AutenticarUsuarioComando comando,
            [FromServices] UsuarioManipulador manipulador)
        {
            var usuarioExistente = await manipulador.Manipular(comando);

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
