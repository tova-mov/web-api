using System.ComponentModel.DataAnnotations;
namespace login
{
    public class User
    {
        public static int Index = 0;


        public int Id { get; set; }
         

        [EmailAddress(ErrorMessage ="Email not valid")]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [StringLength(20, ErrorMessage = "password length must be between 5-20", MinimumLength = 5)]
        public string Password { get; set; }
        
       
    }
}
 