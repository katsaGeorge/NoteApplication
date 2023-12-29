using AutoMapper;
using NoteApp.Data;
using NoteApp.Repositories;

namespace NoteApp.Services
{
    public class ApplicationService : IAppliationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUserService UserService => new UserService(_unitOfWork, _mapper);

        public  INoteService NoteService =>new NoteService(_mapper, _unitOfWork);
    }
}
