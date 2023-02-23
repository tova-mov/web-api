using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        // POST api/<UserController>
        [HttpPost("logIn")]
        public ActionResult<User> LogIn([FromBody] User userFromBody)
        {
            User foundUser = FindUser(userFromBody);
            if (foundUser==null)
                return Unauthorized();
            return Ok(foundUser);

        }

        [HttpPost]
        public ActionResult<User> Register([FromBody] User newUser)
        {
            if (IsUserNameExist(newUser))
                return NoContent();

            int numberOfUsers = System.IO.File.ReadLines("./wwwroot/users.txt").Count();
            newUser.Id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(newUser);
            System.IO.File.AppendAllText("./wwwroot/users.txt", userJson + Environment.NewLine);
            return Ok(newUser);
            // return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);

        }

        private bool IsUserNameExist(User newUser)
        {
            using (StreamReader reader = System.IO.File.OpenText("./wwwroot/users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User userFromFile = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (userFromFile.Email == newUser.Email)
                        return true;
                }
                return false;
            }
        }
        private User FindUser(User user)
        {
            using (StreamReader reader = System.IO.File.OpenText("./wwwroot/users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User userFromFile = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (userFromFile.Email == user.Email && userFromFile.Password == user.Password)
                        return userFromFile;
                }

                return null;
            }
        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText("./wwwroot/users.txt"))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.Id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText("./wwwroot/users.txt");
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText("./wwwroot/users.txt", text);
            }
            return userToUpdate;
        }

        
    }
}
