using AutoMapper;
using NoteApp.Data;

namespace NoteApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NotedbContext _context;

        private readonly IMapper _mapper;

        public UnitOfWork(NotedbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public IBaseRepository<Note> NoteRepository => new NoteRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
