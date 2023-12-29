
using NoteApp.Data;
using NoteApp.DTO;

namespace NoteApp.Repositories
{
    public interface IUserRepository
    {
        Task<UserReadOnlyDTO?> CreateUserAsync(UserCreateDTO userCreateDTO);
        Task<User?> UpdateUserAsync(UserUpdatedDTO userUpdatedDTO);
        
        Task<User?> GetUserAsync(string username, string password); 
    }
}
