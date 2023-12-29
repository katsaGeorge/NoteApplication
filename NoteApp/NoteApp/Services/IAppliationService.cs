using NoteApp.Repositories;

namespace NoteApp.Services
{
    public interface IAppliationService
    {
        IUserService UserService { get; }
        INoteService NoteService {get; }
    }
}
