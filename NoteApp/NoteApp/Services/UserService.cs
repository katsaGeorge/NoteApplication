using AutoMapper;
using NoteApp.Data;
using NoteApp.DTO;
using NoteApp.Repositories;

namespace NoteApp.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserReadOnlyDTO?> CreateUserAsync(UserCreateDTO userdto)
        {
            try
            {


                var user = await _unitOfWork.UserRepository.CreateUserAsync(userdto);
                if (user is null)
                {
                    throw new ApplicationException("UserAlreadyExists");
                }
                await _unitOfWork.SaveAsync();
                
                return user;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
           
        }

        public async Task<User?> LoginUserAsync(UserLoginDTO userdto)
        {
            var user = await _unitOfWork.UserRepository.GetUserAsync(userdto.Username!,
                                                                                    userdto.Password!);

            if(user is null)
            {
                return null;
            }

            return user;



        }

        public async Task<User?> UpdateUserAsync(UserUpdatedDTO userdto)
        {
            var user = await _unitOfWork.UserRepository.UpdateUserAsync(userdto);
            await _unitOfWork.SaveAsync();
            return user;
        }
    }
}