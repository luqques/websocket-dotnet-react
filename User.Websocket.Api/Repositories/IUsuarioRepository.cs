using User.Websocket.Api.Models;

namespace User.Websocket.Api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario user);
        Task<Usuario?> UpdateAsync(int id, Usuario user);
        Task<bool> DeleteAsync(int id);
    }
}
