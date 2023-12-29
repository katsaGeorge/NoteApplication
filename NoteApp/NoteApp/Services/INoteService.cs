using NoteApp.Data;
using NoteApp.DTO;

namespace NoteApp.Services
{
    public interface INoteService 
    {
        Task<NoteReadOnlyDTO?> NoteCreateAsync(NoteCreateDTO noteCreatedto);
        Task<NoteReadOnlyDTO?> NoteUpdateAsync(Note noteToUpdate);
        Task<bool> NoteDeleteAsync (int  id);
    }
}
