
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Domain;

namespace Linked.cardViewModul
{
    public class HomeCardViewModul
    {
        public List<User> LIST_UserToConection = new List<User>();
        public User Use_ThisUser = new User();
    }

    public class SignInViewModel
    {
        [DisplayName("User name Or ID")]
        [Required(ErrorMessage = "Please enter the {0}")]
        public string NameOrId { get; set; }
        public bool RememberMe { get; set; }
    }
}
