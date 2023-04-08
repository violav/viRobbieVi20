
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace Robbie.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService) => this.userService = userService;

        [HttpGet]
        public async Task<List<User>> GetAll([FromHeader] string TenantId) => await userService.GetAllAsync(TenantId);

        [HttpPost]
        public async Task<IActionResult> Post(User newItem)
        {
            await userService.CreateAsync(newItem);
            return CreatedAtAction(nameof(Post), new { id = newItem.Id }, newItem);
        }

        [HttpGet("login/{email}")]
        public async Task<User> GetUserAfterLogin(string email)
        {
            User user = await userService.GetUserByEmail(email);
            if (user is null) user = await userService.CreateAfterLoginAsync(email);
            return user;
        }

        [HttpGet("emails")]
        public async Task<List<User>> GetUsersByEmail([FromQuery] string[] emails) => await userService.GetUsersByEmail(emails);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetOne([FromHeader] string TenantId, string id)
        {
            var item = await userService.GetAsync(id, TenantId);
            return item is null ? NotFound() : item;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<User>> Update([FromHeader] string TenantId, string id, User user)
        {
            var operation = await userService.UpdateAsync(id, user);
            return operation.MatchedCount != 0 ? user : NotFound();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete([FromHeader] string TenantId, string id)
        {
            var result = await userService.RemoveAsync(id);
            return result.DeletedCount != 0 ? NoContent() : NotFound();
        }


    }
}