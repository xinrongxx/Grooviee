using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grooviee.Server.Data;
using Grooviee.Shared.Domain;
using Grooviee.Server.IRepository;

namespace Grooviee.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public DriversController(ApplicationDbContext context)
        public DriversController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Drivers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetDrivers()
        public async Task<ActionResult> GetDrivers()
        {
            //return await _context.Drivers.ToListAsync();
            var Drivers = await _unitOfWork.Drivers.GetAll();
            return Ok(Drivers);
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetDriver(int id)
        public async Task<ActionResult> GetDrivers(int id)
        {
            //var Model = await _context.Drivers.FindAsync(id);
            var Driver = await _unitOfWork.Drivers.Get(q => q.Id == id);

            if (Driver == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Driver);
        }

        // PUT: api/Drivers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, Driver Driver)
        {
            if (id != Driver.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Drivers.Update(Driver);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!DriverExists(id))
                if (!await DriverExists(id))
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

        // POST: api/Drivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostDriver(Driver Driver)
        {
            //_context.Drivers.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Drivers.Insert(Driver);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDriver", new { id = Driver.Id }, Driver);
        }

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            //var Model = await _context.Drivers.FindAsync(id);
            var Driver = await _unitOfWork.Drivers.Get(q => q.Id == id);
            if (Driver == null)
            {
                return NotFound();
            }

            //_context.Drivers.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Drivers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> DriverExists(int id)
        {
            //return _context.Drivers.Any(e => e.Id == id);
            var Driver = await _unitOfWork.Drivers.Get(q => q.Id == id);
            return Driver != null;
        }
    }
}