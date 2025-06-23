using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using User.Websocket.Api.Hubs;
using User.Websocket.Api.Models;
using User.Websocket.Api.Repositories;

namespace User.Websocket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(IUsuarioRepository repo, IHubContext<UsuarioHub> hubContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
            => Ok(await repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await repo.GetByIdAsync(id);
            return usuario is null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(Usuario usuario)
        {
            var criado = await repo.CreateAsync(usuario);
            await hubContext.Clients.All.SendAsync("UsuarioAlterado", "criado", criado);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, Usuario usuario)
        {
            var atualizado = await repo.UpdateAsync(id, usuario);
            if (atualizado is null) return NotFound();

            await hubContext.Clients.All.SendAsync("UsuarioAlterado", "atualizado", atualizado);
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await repo.DeleteAsync(id);
            if (!sucesso) return NotFound();

            await hubContext.Clients.All.SendAsync("UsuarioAlterado", "removido", id);
            return NoContent();
        }
    }
}
