using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagment.Infrastructure;
using UserManagment.Models;
using UserManagment.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IKeysService _keysService;

        public UsersController(IUserService userService, IKeysService keysService)
        {
            _userService = userService;
            _keysService = keysService;
        }

        // GET: api/values
        [HttpGet(Name = nameof(GetAllUsers))]
        [ValidateModel]
        public IEnumerable<User> GetAllUsers()
        {
            return _userService.GetUsers();
        }

        // GET api/users/5
        [HttpGet("{id}", Name = nameof(GetUserById))]
        [ValidateModel]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return new OkObjectResult(user);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // POST api/users
        [HttpPost("create", Name = nameof(CreateUser))]
        [ValidateModel]
        public IActionResult CreateUser([FromBody]User user)
        {
            if (user == null || !user.Id.NotEmpty())
            {
                return BadRequest();
            }

            try
            {
                _userService.CreateUser(user);

                return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("update/{id}")]
        [ValidateModel]
        public IActionResult UpdateUser(string id, [FromBody]User data)
        {
            if (data == null || !id.NotEmpty() || data.Id != id)
            {
                return BadRequest();
            }

            try
            {
                /**/
                var user = _userService.GetUser(id);

                _userService.UpdateUser(data);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("generatekey/{id}")]
        [ValidateModel]
        public IActionResult GenerateKeyForUser(string id)
        {

            if (!id.NotEmpty())
            {
                return BadRequest();
            }

            try
            {
                var user = _userService.GetUser(id);

                _keysService.GenerateKey(user);

                _userService.UpdateUser(user);

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/values/5
        [HttpDelete("delete/{id}")]
        [ValidateModel]
        public IActionResult Delete(string id)
        {

            try
            {
                _userService.DeleteUser(id);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
