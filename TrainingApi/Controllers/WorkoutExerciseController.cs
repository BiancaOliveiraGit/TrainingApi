
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<WorkoutExercise> _logger;

        public WorkoutExerciseController(IRepository repository, ILogger<WorkoutExercise> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/workoutexercise
        [HttpGet]
        public ActionResult<IEnumerable<WorkoutExercise>> Get()
        {
            var   WorkoutExercises = _Repository.GetWorkoutExercises(_logger);
            return Ok(WorkoutExercises);
        }

        // GET api/workoutexercise/5
        [HttpGet("{id}")]
        public ActionResult<WorkoutExercise> Get(int id)
        {
            var   WorkoutExercise = _Repository.GetWorkoutExerciseById(id, _logger);
            return Ok(WorkoutExercise);
        }

        // POST api/workoutexercise
        [HttpPost]
        public ActionResult<WorkoutExercise> Post([FromBody] WorkoutExercise newWorkoutExercise)
        {
            var  postedWorkoutExercise = _Repository.PostNewWorkoutExercise(newWorkoutExercise, _logger);   
            return Ok(postedWorkoutExercise);
        }

        // PUT api/workoutexercise/5
        [HttpPut("{id}")]
        public ActionResult<WorkoutExercise> Put(int id, [FromBody] WorkoutExercise updateWorkoutExercise)
        {
            var  postedWorkoutExercise = _Repository.UpdateWorkoutExercise(id, updateWorkoutExercise, _logger);
            return Ok(postedWorkoutExercise);
        }

        /* // DELETE api/workoutexercise/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
