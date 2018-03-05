using InterviewManagement.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InterviewManagement.Controllers
{
    [Route("api/Candidates")]
    [EnableCors("MyPolicy")]
    public class CandidatesController : Controller
    {
        private readonly CandidateContext _context;

        public CandidatesController(CandidateContext context)
        {
            _context = context;

            if (_context.Candidates.Count() == 0)
            {
                _context.Candidates.Add(new Candidate { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        public IEnumerable<Candidate> GetAll()
        {
            return _context.Candidates.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        [EnableCors("MyPolicy")]
        public IActionResult GetById(long id)
        {
            var item = _context.Candidates.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        [EnableCors("MyPolicy")]
        public IActionResult Create([FromBody] Candidate item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(item.Id.ToString()))
            {
                _context.Candidates.Add(item);
                _context.SaveChanges();

                return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
            }
            else
            {
                return Update(item.Id, item);
            }
        }

        [HttpPost("{id}")]
        //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
        //[EnableCors("MyPolicy")]
        public IActionResult Update(long id, [FromBody] Candidate item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.Candidates.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
          
            todo.Name = item.Name;
            todo.Number = item.Number;
            todo.Skills = item.Skills;
            todo.InterviewDate = item.InterviewDate;
            
            _context.Candidates.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [EnableCors("MyPolicy")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Candidates.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
