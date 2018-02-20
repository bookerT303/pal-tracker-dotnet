using Microsoft.AspNetCore.Mvc;
using System;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : Controller
    {
        private ITimeEntryRepository _repository;
        private readonly IOperationCounter<TimeEntry> _operationCounterObject;

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            _operationCounterObject.Increment(TrackedOperation.Create);
            var createdTimeEntry = _repository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry",
                new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            _operationCounterObject.Increment(TrackedOperation.Read);
            return _repository.Contains(id) ? (IActionResult) Ok(_repository.Find(id)) : NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            _operationCounterObject.Increment(TrackedOperation.List);
            return Ok(_repository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            _operationCounterObject.Increment(TrackedOperation.Update);
            return _repository.Contains(id) ? (IActionResult) Ok(_repository.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _operationCounterObject.Increment(TrackedOperation.Delete);
            if (_repository.Contains(id) == false) {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }

        public TimeEntryController(ITimeEntryRepository repository, IOperationCounter<TimeEntry> operationCounterObject)
        {
            _repository = repository;
            _operationCounterObject = operationCounterObject;
        }
    }
}
