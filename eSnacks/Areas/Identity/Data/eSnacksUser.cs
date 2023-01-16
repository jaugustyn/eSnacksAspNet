using eSnacks.Models;
using Microsoft.AspNetCore.Identity;

namespace eSnacks.Areas.Identity.Data;

// Add profile data for application users by adding properties to the eSnacksUser class
public class eSnacksUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }
    
    [PersonalData]
    public string LastName { get; set; }

    [PersonalData]
    public string Address { get; set; }
    
    [PersonalData]
    public DateTime DateOfBirth { get; set; }
}

