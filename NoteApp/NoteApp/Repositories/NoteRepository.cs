using AutoMapper;
using NoteApp.Data;
using NoteApp.DTO;

namespace NoteApp.Repositories
{
    public class NoteRepository : BaseRepository<Note>
    {
       
        public NoteRepository(NotedbContext context) : base(context)
        {
            
        }

       /* public override Task<Note?> GetAsync(int id)
        {
            return base.GetAsync(id);
        }*/

        public async override Task AddAsync(Note entity)
        {
             await base.AddAsync(entity);
        }

       /* public async Task<UserReadOnlyDTO?> CreateNoteAsync(UserCreateDTO userCreateDTO)
        {
                var user  = _mappper.Map<User>(userCreateDTO);
                
            await _context.Users.AddAsync(user);
        }*/

        public override Task<bool> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }

        public override Task<IEnumerable<Note>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public override void UpdateAsync(Note entity)
        {
            base.UpdateAsync(entity);
        }

        
    }
}
