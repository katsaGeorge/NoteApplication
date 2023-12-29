using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.DTO;
using NoteApp.Services;
using static Azure.Core.HttpHeader;

namespace NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppliationService _appService;
        private readonly NotedbContext _context;
        private readonly IMapper _mapper;

        public UsersController(NotedbContext context, IMapper mapper, IAppliationService service)
        {
            _context = context;
            _mapper = mapper;
            _appService = service;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<UserReadOnlyDTO>>(users);

            return Ok(usersDto);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var userDto = _mapper.Map<UserReadOnlyDTO>(user);
            if (_context.Users == null || user is null)
            {
                return NotFound();
            }


            return userDto;
        }

        [HttpGet("allNotes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetUserNotes(int userId)
        {
            var userNotes = await _context.Notes.Where(n => n.UserId == userId).ToListAsync();

            if (userNotes == null || userNotes.Count == 0)
            {
                return NotFound("Δεν βρέθηκαν σημειώσεις για αυτόν τον χρήστη.");
            }

            return  Ok(userNotes);
        }
                
            
            




            // PUT: api/Users/5
            [HttpPut("{id}")]
        
        public async Task<ActionResult<UserUpdatedDTO>> PutUser(int id, UserUpdatedDTO userdto)
        {
            
            if (id != userdto.Id)
            {
                return BadRequest();
            }
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
            if (user == null) { return NotFound(); }

           var updateduser = _mapper.Map<UserUpdatedDTO>(user);
            var userToUpdate = _mapper.Map<User>(updateduser);
        

           

            try
            {
                _context.Attach(userToUpdate);
                _context.Entry(userToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem("Internal Server Error");
            }

            return updateduser; // this returns ok with no answer
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreateDTO usercreateDto)
        { 
            if (usercreateDto.Password != usercreateDto.ConfPassword)
            {
                return BadRequest("Passwords doesn't match");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == usercreateDto.Username);
            if(user is not null)
            {
                return BadRequest(new { errors = new { message = "Username Already Exsists!" } });
            }

            try
            {
                var usertoCreated =  await _appService.UserService.CreateUserAsync(usercreateDto);
                //  var created = _mapper.Map<UserReadOnlyDTO>(user);

                var userCreated = await _context.Users.FirstOrDefaultAsync(x => x.Username == usertoCreated!.Username);

              return CreatedAtAction(nameof(GetUser),new {id = userCreated!.Id},userCreated);
            }catch{
                return BadRequest();
            }
        }


        [HttpPost("login")]
        
        public async Task<ActionResult<User>> LoginUser(UserLoginDTO dto)
        {
            if (dto is null)
            {
                return BadRequest("Please fill the textfields correct");
            }

            var user = await _appService.UserService.LoginUserAsync(dto);
            if(user is null)
            {
                return BadRequest(new { errors = new { message = "Wrong credentials" } });
            }
            return Ok(user);
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       /* private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
