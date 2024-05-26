using System.ComponentModel.DataAnnotations;
using CodeCrafters_backend_teamwork.src.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeCrafters_backend_teamwork.src.Entities;


[Index(nameof(Email), IsUnique = true)]
public class User
{

    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
   
    public string Password { get; set; }
    
   
    public string Email { get; set; }
    
    public Guid Id { get; set; }
   
  
    public string PhoneNumber { get; set; }

    public Role Role { get; set; }
}
