using Microsoft.AspNetCore.Identity;

namespace eSnacks.Models;

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
    
    public IList<UserOrders> UserOrders { get; set; }
}

