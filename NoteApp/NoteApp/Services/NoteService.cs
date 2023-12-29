using AutoMapper;
using NoteApp.Data;
using NoteApp.DTO;
using NoteApp.Repositories;

namespace NoteApp.Services
{
    public class NoteService :  INoteService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public NoteService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<NoteReadOnlyDTO?> NoteCreateAsync(NoteCreateDTO noteCreatedto)
        {
            var note = _mapper.Map<Note>(noteCreatedto);
            await  _unitOfWork.NoteRepository.AddAsync(note);
            var readnote = _mapper.Map<NoteReadOnlyDTO>(noteCreatedto);
            await _unitOfWork.SaveAsync();
            return readnote;
        }

        public async Task<bool> NoteDeleteAsync(int id)
        {
            if (await _unitOfWork.NoteRepository.DeleteAsync(id))
            {
               await  _unitOfWork.SaveAsync();
                return true;
            }
           return false;
        }

        public async Task<NoteReadOnlyDTO?> NoteUpdateAsync(Note noteUpdatedto)
        {
           if (noteUpdatedto is null)
            {
                return null!;
            }
            
             _unitOfWork.NoteRepository.UpdateAsync(noteUpdatedto);
            await _unitOfWork.SaveAsync();
            var updatedNote = _mapper.Map<NoteReadOnlyDTO>(noteUpdatedto);
            return updatedNote;
        }
    }
}
