using DatingApp.API.Databases.Entities;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_usersService.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _usersService.GetUserById(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            _usersService.CreateUser(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User user)
        {
            var updateUser = _usersService.GetUserById(id);
            if (updateUser is null) return NotFound();
            _usersService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deleteUser = _usersService.GetUserById(id);
            if (deleteUser is null) return NotFound();
            _usersService.DeleteUser(id);
            return Ok();
        }
    }
}