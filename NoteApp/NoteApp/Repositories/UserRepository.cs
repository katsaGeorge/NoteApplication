using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using NoteApp.Data;
using NoteApp.DTO;
using NoteApp.Security;

namespace NoteApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly IMapper _mappper;
        public UserRepository(NotedbContext context, IMapper mapper) : base(context)
        {
            _mappper = mapper;
        }

        public async Task<UserReadOnlyDTO?> CreateUserAsync(UserCreateDTO userdto)
        {       
            var existUsername = await _context.Users.FirstOrDefaultAsync(x => x.Username == userdto.Username);
            if (existUsername != null) return null;

            var user = _mappper.Map<User>(userdto);
            var userreaddto = _mappper.Map<UserReadOnlyDTO>(user);
            user.Password = EncryptionUtil.Encrypt(user.Password);
           
            await _context.Users.AddAsync(user);
         
            return userreaddto;

        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            if (!EncryptionUtil.IsValidPassword(password, user.Password)) return null;

            return user;
                
                
            

        }

        public async Task<User?> GetUserByUsername(string username)
        {
           return await _context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }


        public async Task<User?> UpdateUserAsync(UserUpdatedDTO userdto)
        {
            var user = await _context.Users.FindAsync(userdto.Username);

            if (user is not null) return null;

            var userToUpdated = _mappper.Map<User>(userdto);

             _context.Users.Update(userToUpdated);
            return user;
        }
            
    }
}
