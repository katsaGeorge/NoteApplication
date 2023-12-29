using NoteApp.Data;
using NoteApp.DTO;

namespace NoteApp.Services
{
    public interface IUserService
    {
        Task<UserReadOnlyDTO?> CreateUserAsync(UserCreateDTO userdto);
        Task<User?> LoginUserAsync(UserLoginDTO userdto);
        Task<User?> UpdateUserAsync(UserUpdatedDTO userdto);


    }
}
