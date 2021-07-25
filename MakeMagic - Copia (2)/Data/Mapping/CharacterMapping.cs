using MakeMagic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MakeMagic.Data.Mapping
{
    public class CharacterMapping
    {
        public CharacterMapping(EntityTypeBuilder<Character> entityBuilder)
        {
            entityBuilder.HasKey(tb => new { tb.Id });
            InsertFirstCharacter(entityBuilder);
        }
        private void InsertFirstCharacter(EntityTypeBuilder<Character> entityBuilder)
        {
            DateTime dt = new DateTime(2021, 06, 08);
            entityBuilder.HasData(
                new Character()
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Role = "student",
                    School = "Hogwarts School of Witchcraft and Wizardry",
                    House = "1760529f-6d51-4cb1-bcb1-25087fce5bde",
                    Patronus = "stag"
                });
        }
    }
}
