using SocialNetworkFinalProject.Models;
using System.Collections.Generic;

namespace SocialNetworkFinalProject.ViewModels.Account
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            User = user;
        }

        public User User { get; set; }
        public List<User> Friends { get; set; }

    }
}
