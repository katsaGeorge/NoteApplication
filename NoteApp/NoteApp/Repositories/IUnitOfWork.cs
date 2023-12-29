using NoteApp.Data;

namespace NoteApp.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBaseRepository<Note> NoteRepository { get; }

        public Task<bool> SaveAsync();
    }

        
}
