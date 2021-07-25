using AutoMapper;
using MakeMagic.Data;
using MakeMagic.Data.DTOs;
using MakeMagic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeMagic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterDto characterDto)
        {
            Character character = _mapper.Map<Character>(characterDto);
            _context.Character.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SelectById), new { character.Id }, character);
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get()
        {
            return Ok(_context.Character);
        }

        [HttpGet("{id:int}")]
        public IActionResult SelectById(int id)
        {
            var character = _context.Character.FirstOrDefault(c => c.Id == id);

            if (character != null)
            {
                return Ok(character);
            }

            return NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacterDto characterDTO)
        {
            Character character = (Character)SelectById(id).;
            if (character == null)
            {
                return NotFound();
            }
            _mapper.Map(characterDTO, character);
            await _context.SaveChangesAsync();
            return NoContent();            
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Character character = (Character)SelectById(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Remove(character);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
