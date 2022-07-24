using clan_system.Models.Entities;
using System.Collections.Generic;

namespace clan_system.Models.ViewModels
{
    public class ContributionViewModel
    {
        public string LoggedUserName { get; set; }
        public string ClanName { get; set; }
        public List<User> ClanUsers { get; set; }
    }
}
