using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserWebAPI.Data;
using UserWebAPI.Models;

namespace UserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UsersController(IUserRepository repo )
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            return Ok(user);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser(Models.User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.UserID <= 0)
            {
                return BadRequest();
            }

            var userForUpdate = await _repo.GetUser(model.UserID);

            if (userForUpdate == null)
            {
                return NotFound();
            }

            // Se puede usar un mapeer
            userForUpdate.UserName = model.UserName;
            userForUpdate.Name = model.Name;
            userForUpdate.Email = model.Email;
            userForUpdate.Phone = model.Phone;

            if (await _repo.Update())
                return NoContent();

            throw new Exception($"Updating user {model.UserID.ToString()} failed to save");

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] Models.User model) {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _repo.UserExists(model.UserName))
            {
                return BadRequest("User already exists");
            }

            var userToCreate = new Models.User
            {
                UserName = model.UserName,
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };

            var createdUser = await _repo.Create(userToCreate);

            return StatusCode(201);



        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id) {
            await _repo.Delete(id);
            return Ok();
        }

    }
}