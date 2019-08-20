using CloudAuditionApi.DatabaseService;
using CloudAuditionApi.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudAuditionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageDbService _service;

        public MessagesController(IMessageDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetAsync(long id)
        {
            var message = await _service.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Message message)
        {
            await _service.CreateAsync(message);

            return CreatedAtAction(nameof(GetAsync), new { id = message.Id }, message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var message = await _service.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(message);

            return NoContent();
        }
    }
}