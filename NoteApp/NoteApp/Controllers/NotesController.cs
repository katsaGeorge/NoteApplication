using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.DTO;
using NoteApp.Repositories;
using NoteApp.Services;

namespace NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotedbContext _context;
        private readonly IAppliationService _appService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NotesController(NotedbContext context,IUnitOfWork unitOfWork, IAppliationService service, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _appService = service;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
          if (_context.Notes == null)
          {
              return NotFound();
          }
           var notes =  await _context.Notes.ToListAsync();
            List<Note> datedNotes = notes.OrderBy(note => note.Date).ToList();
            return Ok(datedNotes);
        }

        

        

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
          if (_context.Notes == null)
          {
              return NotFound();
          }
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteUpdatedDTO dto)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) 
            {
                return BadRequest("Note doesn't exist");
            }

            note.Text = dto.Text;
            note.Subject = dto.Subject;

            await _appService.NoteService.NoteUpdateAsync(note);
            return Ok("Note Updated");
                
        }

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  async Task<ActionResult<Note>> PostNote(NoteCreateDTO noteDto)
        {
            try{
                var note = _mapper.Map<Note>(noteDto);
               // noteDto.date = DateTime.Now;

                var created = await _appService.NoteService.NoteCreateAsync(noteDto);          
               await _unitOfWork.SaveAsync();
                return note;
            }
            catch
            {
                return BadRequest();
            }

        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            if (_context.Notes == null)
            {
                return NotFound(); 
            }
            
            if (await _appService.NoteService.NoteDeleteAsync(id))
            {
                return Ok("Your note deleted successfully");
            }

            
            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return (_context.Notes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
