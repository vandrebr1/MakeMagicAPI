using AutoMapper;
using MakeMagic.Data.DTOs;
using MakeMagic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
