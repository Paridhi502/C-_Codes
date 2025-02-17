using Microsoft.AspNetCore.Mvc;

namespace ServiceLifetimeDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ISingletonCounter _singletonCounter;
        private readonly IScopedCounter _scopedCounter;
        private readonly ITransientCounter _transientCounter;

        public CounterController(ISingletonCounter singletonCounter, IScopedCounter scopedCounter, ITransientCounter transientCounter)
        {
            _singletonCounter = singletonCounter;
            _scopedCounter = scopedCounter;
            _transientCounter = transientCounter;
        }

        [HttpGet("increment/{counterType}")]
        public IActionResult Increment(string counterType)
        {
            switch (counterType.ToLower())
            {
                case "singleton":
                    _singletonCounter.Increment();
                    break;
                case "scoped":
                    _scopedCounter.Increment();
                    break;
                case "transient":
                    _transientCounter.Increment();
                    break;
                default:
                    return BadRequest("Invalid counter type");
            }

            return Ok(new
            {
                Singleton = _singletonCounter.Count,
                Scoped = _scopedCounter.Count,
                Transient = _transientCounter.Count
            });
        }
    }
}
