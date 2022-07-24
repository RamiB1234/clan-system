using clan_system.Models.Entities;
using System.Collections.Generic;

namespace clan_system.Models.ViewModels
{
    public class LandingPageViewModel
    {
        public string LoggedUserName { get; set; }
        public List<Clan> ClanList { get; set; }
        public bool UserInClan { get; set; }
    }
}
