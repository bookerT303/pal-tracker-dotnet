using Microsoft.AspNetCore.Mvc;
using System;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : Controller
    {
        private ITimeEntryRepository _repository;

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _repository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry",
                new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            return _repository.Contains(id) ? (IActionResult) Ok(_repository.Find(id)) : NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_repository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            return _repository.Contains(id) ? (IActionResult) Ok(_repository.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (_repository.Contains(id) == false) {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }

        public TimeEntryController(ITimeEntryRepository repository)
        {
            _repository = repository;
        }
    }
}
