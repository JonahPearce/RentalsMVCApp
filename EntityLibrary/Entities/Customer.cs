using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EntityLibrary.Entities
{
    public class Customer : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<Property>? OwnedProperties { get; set; }


    }
}
