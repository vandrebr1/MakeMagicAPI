using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MakeMagic.Data.DTOs
{
    /// <summary>
    /// Classe represent Data Transfer Object
    /// </summary>
    public class CharacterDto
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} - This field is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string Name { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} - This field is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        [DisplayName("Role")]
        public string Role { get; set; }

        [Display(Name = "School")]
        [Required(ErrorMessage = "{0} - This field is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string School { get; set; }

        [Display(Name = "House")]
        [Required(ErrorMessage = "{0} - This field is required!")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "{0}'s length should be {1}.")]
        public string House { get; set; }

        [Display(Name = "Patronus")]
        [Required(ErrorMessage = "{0} - This field is required!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string Patronus { get; set; }
    }
}
