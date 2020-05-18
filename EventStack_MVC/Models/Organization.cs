using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventStack_MVC.Interfaces;

namespace EventStack_MVC.Models
{
    public class Organization : IDbModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password must be set!")]
        [StringLength(30, ErrorMessage = "The maximum number of character is 30!")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-+,\(\)]).{8,}$", ErrorMessage =
            "Password must contain at least 1 lowercase and uppercase alphabetical character, 1 numeric character, 1 special character(!,@,#,$,%,^,&,*) and must be eight characters or longer!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email must be set!")]
        [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
        [RegularExpression(@"\A(?:[a-zA-Z])(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage =
            "Email must contain eg. example@example.com")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }

        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        public string Description { get; set; }

        public IEnumerable<string> EventsId { get; set; }

        [RegularExpression("^[0-9]{10}$", ErrorMessage = "NIP must contain 10 digit!")]
        public string NIP { get; set; }

        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "REGON must contain 9 digit!")]
        public string REGON { get; set; }

        [RegularExpression(@"[A-z0-9]{32}", ErrorMessage = "The maximum number of characters is 32! Only characters from \'A\' to \'z\' and from \'0\' to \'9\'")]
        public string Secret { get; set; }
    }
}