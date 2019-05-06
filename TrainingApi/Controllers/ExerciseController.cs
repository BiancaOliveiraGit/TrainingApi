
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<Exercise> _logger;

        public ExerciseController(IRepository repository, ILogger<Exercise> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/exercise
        [HttpGet]
        public ActionResult<IEnumerable<Exercise>> Get()
        {
            var  exercises = _Repository.GetExercises(_logger);
            return Ok(exercises);
        }

        // GET api/exercise/5
        [HttpGet("{id}")]
        public ActionResult<Exercise> Get(int id)
        {
            var exercise = _Repository.GetExerciseById(id, _logger);
            return Ok(exercise);
        }

        // POST api/exercise
        [HttpPost]
        public ActionResult<Exercise> Post([FromBody] Exercise newExercise)
        {
            var postedExercise = _Repository.PostNewExercise(newExercise, _logger);         
            return Ok(postedExercise);
        }

        // PUT api/exercise/5
        [HttpPut("{id}")]
        public ActionResult<Exercise> Put(int id, [FromBody] Exercise updateExercise)
        {
           var postedExercise = _Repository.UpdateExercise(id, updateExercise, _logger);
           return Ok(postedExercise);
        }

        /* // DELETE api/exercise/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
