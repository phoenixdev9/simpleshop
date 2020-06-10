using System;

namespace Domains.Entities {
    public class Address {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}