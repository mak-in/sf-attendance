using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SfAttendance.Server;
using SfAttendance.Server.Entities;

namespace SfAttendance.Server.Controllers.api
{
    [Produces("application/json")]
    [Route("api/WorkingRecords")]
    public class WorkingRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkingRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkingRecords
        [HttpGet]
        public IEnumerable<WorkingRecord> GetWorkRecords()
        {
            return _context.WorkRecords;
        }

        // GET: api/WorkingRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkingRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workingRecord = await _context.WorkRecords.SingleOrDefaultAsync(m => m.Id == id);

            if (workingRecord == null)
            {
                return NotFound();
            }

            return Ok(workingRecord);
        }

        // PUT: api/WorkingRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkingRecord([FromRoute] int id, [FromBody] WorkingRecord workingRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workingRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(workingRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingRecordExists(id))
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

        // POST: api/WorkingRecords
        [HttpPost]
        public async Task<IActionResult> PostWorkingRecord([FromBody] WorkingRecord workingRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkRecords.Add(workingRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkingRecord", new { id = workingRecord.Id }, workingRecord);
        }

        // DELETE: api/WorkingRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkingRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workingRecord = await _context.WorkRecords.SingleOrDefaultAsync(m => m.Id == id);
            if (workingRecord == null)
            {
                return NotFound();
            }

            _context.WorkRecords.Remove(workingRecord);
            await _context.SaveChangesAsync();

            return Ok(workingRecord);
        }

        private bool WorkingRecordExists(int id)
        {
            return _context.WorkRecords.Any(e => e.Id == id);
        }
    }
}