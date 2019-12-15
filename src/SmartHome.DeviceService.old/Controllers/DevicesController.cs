using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Models;
using SmartHome.DeviceService.Dtos;
using SmartHome.Infrastructure.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace SmartHome.DeviceService.Controllers
{
    /// <summary>
    ///     Manages devices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly SmartHomeContext _context;

        public DevicesController(SmartHomeContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Gets all devices
        /// </summary>
        /// <returns>All devices</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Device> GetDevices()
        {
            return _context.Devices;
        }

        /// <summary>
        /// Gets a device
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns>Device with given id</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        /// <summary>
        ///     Updates a device
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="dto">Data transfer object</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] UpdateDeviceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            var device = await _context.FindAsync<Device>(dto.Id);
            device = dto.Adapt(device);

            device.IPv4Address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> PostDevice([FromBody] RegisterDeviceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = dto.Adapt<Device>();

            var ipv4 = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            device.IPv4Address = ipv4.ToString();

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}