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
    public class MakesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public MakesController(ApplicationDbContext context)
        public MakesController(IUnitOfWork unitofWork)
        {
            //_context = context;
            _unitOfWork = unitofWork;
        }

        // GET: api/Makes
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Model>>> GetMakes()
        public async Task<ActionResult> GetMakes()
        {
            //return await _context.Makes.ToListAsync();
            var makes = await _unitOfWork.Makes.GetAll();
            return Ok(makes);
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Model>> GetMake(int id)
        public async Task<ActionResult> GetMakes(int id)
        {
            //var Model = await _context.Makes.FindAsync(id);
            var make = await _unitOfWork.Makes.Get(q=> q.Id ==id);

            if (make == null)
            {
                return NotFound();
            }

            //return Model;
            return Ok(make);
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Model).State = EntityState.Modified;
            _unitOfWork.Makes.Update(make);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MakeExists(id))
                if (!await MakeExists(id))
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

        // POST: api/Makes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostMake(Make make)
        {
            //_context.Makes.Add(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Makes.Insert(make);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            //var Model = await _context.Makes.FindAsync(id);
            var make = await _unitOfWork.Makes.Get(q=>q.Id ==id);
            if (make == null)
            {
                return NotFound();
            }

            //_context.Makes.Remove(Model);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Makes.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> MakeExists(int id)
        {
            //return _context.Makes.Any(e => e.Id == id);
            var make = await _unitOfWork.Makes.Get(q=>q.Id ==id);
            return make != null;
        }
    }
}
