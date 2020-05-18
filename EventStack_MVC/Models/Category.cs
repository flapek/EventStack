using EventStack_MVC.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventStack_MVC.Models
{
    public class Category : IDbModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name must be set!")]
        [StringLength(50, ErrorMessage = "The maximum number of character is 50!")]
        public string Name { get; set; }
    }
}