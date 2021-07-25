using AutoMapper;
using MakeMagic.Data;
using MakeMagic.Data.DTOs;
using MakeMagic.HttpClients;
using MakeMagic.Models;
using MakeMagic.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeMagic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly HouseApiClient _houseApiClient;
        public CharacterController(IMapper mapper, HouseApiClient houseApiClient)
        {
            _mapper = mapper;
            _houseApiClient = houseApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromServices] DataContext context, [FromBody] CharacterDto characterDto)
        {

            Character character = _mapper.Map<Character>(characterDto);
            var houses = await _houseApiClient.SelectAllAsync();

            if (houses.Exists(character.House))
            {
                context.Character.Add(character);
                await context.SaveChangesAsync();
            }
            else
            {
                return NotFound($"House id: {character.House}, not found");
            }

            return CreatedAtAction(nameof(SelectByIdAsync), new { character.Id }, character);

        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> SelectAllAsync([FromServices] DataContext context, [FromQuery] CharacterFilter characterFilter)
        {
            var character = await context.Character.ApplyFilter(characterFilter).ToListAsync();
            return Ok(character);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Character>> SelectByIdAsync([FromServices] DataContext context, int id)
        {
            var character = await context.Character.FirstOrDefaultAsync(c => c.Id == id);

            if (character != null)
            {
                return Ok(character);
            }

            return NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromServices] DataContext context, int id, [FromBody] CharacterDto characterDTO)
        {
            Character character = await context.Character.FirstOrDefaultAsync(c => c.Id == id);

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
                    await context.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound($"House id: {characterDTO.House}, not found");
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices] DataContext context, int id)
        {
            var character = await context.Character.FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            context.Remove(character);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
