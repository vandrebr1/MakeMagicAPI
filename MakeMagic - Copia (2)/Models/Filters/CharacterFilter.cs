using System.Linq;

namespace MakeMagic.Models.Filters
{
    public static class CharacterFilterExtension
    {
        public static IQueryable<Character> ApplyFilter(this IQueryable<Character> query, CharacterFilter characterFilter)
        {
            if (characterFilter != null)
            {
                if (!string.IsNullOrEmpty(characterFilter.House))
                {
                    query = query.Where(q => q.House.Contains(characterFilter.House));
                }
            }

            return query;
        }
    }

    public class CharacterFilter
    {
        public string House { get; set; }
    }
}
