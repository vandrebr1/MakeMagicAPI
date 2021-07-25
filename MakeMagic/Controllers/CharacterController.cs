using AutoMapper;
using MakeMagic.Data;
using MakeMagic.Data.DTOs;
using MakeMagic.HttpClients;
using MakeMagic.Models;
using MakeMagic.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeMagic.Controllers
{
    /// <summary>
    /// Controller character
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHouseApiClient _houseApiClient;
        private readonly DataContext _context;
        public CharacterController([FromServices] DataContext context, IMapper mapper, IHouseApiClient houseApiClient)
        {
            _mapper = mapper;
            _houseApiClient = houseApiClient;
            _context = context;
        }

        /// <summary>
        /// Create a new character using body from request
        /// </summary>
        /// <param name="characterDto">DTO character</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new character.")]
        [ProducesResponseType(statusCode: 201, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 500, Type = typeof(Character))]
        [SwaggerResponse(statusCode: 404, Description = "House id: {character.House}, not found")]
        public async Task<IActionResult> Create([FromBody] CharacterDto characterDto)
        {
            Character character = _mapper.Map<Character>(characterDto);
            var houses = await _houseApiClient.SelectAllAsync();

            if (houses.Exists(character.House))
            {
                _context.Character.Add(character);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound($"House id: {character.House}, not found");
            }

            return CreatedAtAction(nameof(SelectById), new { character.Id }, character);
        }

        /// <summary>
        /// Return all characters, filter <see cref="CharacterFilter"/> is not required
        /// </summary>
        /// <param name="characterFilter">Use houseId to filter query</param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Select all characters. Filter is optional.")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 500, Type = typeof(Character))]
        [HttpGet]
        public async Task<ActionResult<List<Character>>> SelectAll([FromQuery, SwaggerParameter("Search keywords", Required = false)] CharacterFilter characterFilter)
        {
            var character = await _context.Character.ApplyFilter(characterFilter).ToListAsync();
            return Ok(character);
        }

        /// <summary>
        /// Return one character filter by <see cref="Character.Id"/>
        /// </summary>
        /// <param name="id">Character <see cref="Character.Id"/></param>
        /// <returns>Character</returns>
        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Return one character by Id")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 500, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<Character>> SelectById(int id)
        {
            var character = await _context.Character.FirstOrDefaultAsync(c => c.Id == id);

            if (character != null)
            {
                return Ok(character);
            }

            return NotFound();
        }


        /// <summary>
        /// Update a character existing in database
        /// </summary>
        /// <param name="id"><see cref="Character.Id"/></param>
        /// <param name="characterDTO">DTO character from body request</param>
        /// <returns>statusCode: 204</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 404)]
        [SwaggerOperation(Summary = "Update exist character by Id")]
        public async Task<IActionResult> Update(int id, [FromBody] CharacterDto characterDTO)
        {
            Character character = await _context.Character.FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }
            else
            {
                var houses = await _houseApiClient.SelectAllAsync();

                if (houses.Exists(characterDTO.House))
                {
                    _mapper.Map(characterDTO, character);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound($"House id: {characterDTO.House}, not found");
                }
            }
        }


        /// <summary>
        /// Delete character by <see cref="Character.Id"/>
        /// </summary>
        /// <param name="id"><see cref="Character.Id"/></param>
        /// <returns>statusCode: 204</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500, Type = typeof(Character))]
        [ProducesResponseType(statusCode: 404)]
        [SwaggerOperation(Summary = "Delete a character by Id")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = await _context.Character.FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            _context.Remove(character);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
