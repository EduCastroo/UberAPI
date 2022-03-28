using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UberAPI.Model;
using UberAPI.Repository;

namespace UberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            return await _userRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUsers([FromBody] User user)
        {
            var newUser = await _userRepository.Create(user);
            return CreatedAtAction(nameof(GetUsers), new {id = newUser.Id}, newUser);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userToDelete = await _userRepository.Get(id);

            if (userToDelete == null)
                return NotFound();

            await _userRepository.Delete(userToDelete.Id);
            return NoContent();
            
        }
        [HttpPut]
        public async Task<ActionResult> PutUsers(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest();
                await _userRepository.Update(user);
            return NoContent();
        }


    }
    
}