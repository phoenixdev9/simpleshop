using Microsoft.AspNetCore.Identity;

namespace Domains.Entities {
    public class User : IdentityUser {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}