
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Linked.cardViewModul
{
    public class HomeCardViewModel
    {
        public List<User> Suggests = new List<User>();
        public User User = new User();
    }

    public class SignInViewModel
    {
        [DisplayName("User name Or ID")]
        [Required(ErrorMessage = "Please enter the {0}")]
        public string NameOrId { get; set; }
        public bool RememberMe { get; set; }
    }

    public class SignUpViewModel
    {

        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public string UniversityLocation { get; set; }

        public string Field { get; set; }

        public string WorkPlace { get; set; }

        public IFormFile Profile { get; set; }

        public string UserSpecialties { get; set; }

    }

    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public string UniversityLocation { get; set; }

        public string Field { get; set; }

        public string WorkPlace { get; set; }

        public IFormFile Profile { get; set; }
        public string OldProfile { get; set; }
        public List<Domain.SpecialtyUser> OldSpecialty { get; set; }
        public string Specialty { get; set; }

    }

}
