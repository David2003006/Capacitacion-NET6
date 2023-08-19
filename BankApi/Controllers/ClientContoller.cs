using Microsoft.AspNetCore.Mvc;
using BankApi.Services;
using BankApi.Data.BankModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BankApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _services;

        public ClientController(ClientServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _services.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByID(int id)
        {
            var FoundUser = await _services.GetById(id);

            if (FoundUser is null)
                return NotFound(); 

            return FoundUser;
        }

        [HttpPost]
        public async Task<IActionResult> create(User user)
        {
            var newClient = await _services.create(user);

            return CreatedAtAction(nameof(GetByID), new{id= newClient.Id}, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if(id != user.Id)
                return BadRequest();
            
            var ClientUpdate= await GetByID(id);

            if(ClientUpdate is not null )
            {
                await _services.Update(user);
                return NoContent();
            }else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            var ClientToDelete = await GetByID(id);

            if(ClientToDelete is not null)
            {
                await _services.Delete(id);
                return Ok();
            }else
            {
                return NotFound();
            }
        }
    }
}
