using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSecurity.POCOs
{
    public class UserProfile
    {
        public string UserId { get; set; } //supplied by the AspNetUser
        public string UserName { get; set; } //supplied by the AspNetUser
        public int? EmployeeId { get; set; } //this is from the application user
        public int? CustomerId { get; set; }  //this is from the application user
        public string FirstName { get; set; } //supplied by the AspNetUser
        public string LastName { get; set; } //from the Employee
        public string Email { get; set; } //from the Employee
        public bool EmailConfirmation { get; set; } //supplied by the AspNetUser
        public string RequestedPassord { get; set; } //supplied by the AspNetUser
        public IEnumerable<string> RoleMemberships { get; set; } //SecurityRules
    }
}
