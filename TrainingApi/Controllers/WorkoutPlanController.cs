
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<WorkoutPlan> _logger;

        public WorkoutPlanController(IRepository repository, ILogger<WorkoutPlan> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/workoutplan
        [HttpGet]
        public ActionResult<IEnumerable<WorkoutPlan>> Get()
        {
            var  workoutPlans = _Repository.GetWorkoutPlans(_logger);
            return Ok(workoutPlans);
        }

        // GET api/workoutplan/5
        [HttpGet("{id}")]
        public ActionResult<WorkoutPlan> Get(int id)
        {
             var   workoutPlan = _Repository.GetWorkoutPlanById(id, _logger);
             return Ok(workoutPlan);
        }

        // POST api/workoutplan
        [HttpPost]
        public ActionResult<WorkoutPlan> Post([FromBody] WorkoutPlan newWorkoutPlan)
        {
            var  postedWorkoutPlan = _Repository.PostNewWorkoutPlan(newWorkoutPlan, _logger);
            return Ok(postedWorkoutPlan);
        }

        // PUT api/workoutplan/5
        [HttpPut("{id}")]
        public ActionResult<WorkoutPlan> Put(int id, [FromBody] WorkoutPlan updateWorkoutPlan)
        {
            var  postedWorkoutPlan = _Repository.UpdateWorkoutPlan(id, updateWorkoutPlan, _logger);
            return Ok(postedWorkoutPlan);
        }

        /* // DELETE api/workoutplan/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
