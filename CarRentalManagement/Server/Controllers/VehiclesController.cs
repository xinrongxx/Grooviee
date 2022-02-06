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
    public class VehiclesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public VehiclesController(ApplicationDbContext context)
        public VehiclesController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Vehicles
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetVehicles()
        public async Task<ActionResult> GetVehicles()
        {
            //return await _context.Vehicles.ToListAsync();
            var Vehicles = await _unitOfWork.Vehicles.GetAll(includes: q => q.Include(x => x.Make).Include(x => x.Model).Include(x => x.Colour));
            return Ok(Vehicles);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetVehicle(int id)
        public async Task<ActionResult> GetVehicles(int id)
        {
            //var Model = await _context.Vehicles.FindAsync(id);
            var Vehicle = await _unitOfWork.Vehicles.Get(q=> q.Id ==id);

            if (Vehicle == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(Vehicle);
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle Vehicle)
        {
            if (id != Vehicle.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Vehicles.Update(Vehicle);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!VehicleExists(id))
                if (!await VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostVehicle(Vehicle Vehicle)
        {
            //_context.Vehicles.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Vehicles.Insert(Vehicle);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetVehicle", new { id = Vehicle.Id }, Vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            //var Model = await _context.Vehicles.FindAsync(id);
            var Vehicle = await _unitOfWork.Vehicles.Get(q=>q.Id ==id);
            if (Vehicle == null)
            {
                return NotFound();
            }

            //_context.Vehicles.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Vehicles.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> VehicleExists(int id)
        {
            //return _context.Vehicles.Any(e => e.Id == id);
            var Vehicle = await _unitOfWork.Vehicles.Get(q=>q.Id ==id);
            return Vehicle != null;
        }
    }
}
