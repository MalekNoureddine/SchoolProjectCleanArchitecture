using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace CleanArchProject.Data.Entities.Identities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
