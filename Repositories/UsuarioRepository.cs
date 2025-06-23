using Microsoft.EntityFrameworkCore;
using User.Websocket.Api.Data;
using User.Websocket.Api.Models;

namespace User.Websocket.Api.Repositories
{
    public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
    {
        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await context.Usuarios.AsNoTracking().ToListAsync();

        public async Task<Usuario?> GetByIdAsync(int id) =>
            await context.Usuarios.FindAsync(id);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> UpdateAsync(int id, Usuario usuario)
        {
            var existente = await context.Usuarios.FindAsync(id);
            if (existente is null) return null;

            existente.Nome = usuario.Nome;
            existente.Email = usuario.Email;
            await context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario is null) return false;

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
