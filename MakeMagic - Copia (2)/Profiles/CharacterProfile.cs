using AutoMapper;
using MakeMagic.Data.DTOs;
using MakeMagic.Models;

namespace MakeMagic.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CharacterDto, Character>();
        }
    }
}
