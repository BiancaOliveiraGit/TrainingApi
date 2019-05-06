using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientExerciseController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<ClientExercise> _logger;

        public ClientExerciseController(IRepository repository, ILogger<ClientExercise> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/clientexercise
        [HttpGet]
        public ActionResult<IEnumerable<ClientExercise>> Get()
        {
            var clientExercises = _Repository.GetClientExercises(_logger); 
            return Ok(clientExercises);
        }

        // GET api/clientexercise/5
        [HttpGet("{id}")]
        public ActionResult<ClientExercise> Get(int id)
        {
            var clientExercise = _Repository.GetClientExerciseById(id, _logger);            
            return Ok(clientExercise);
        }

        // POST api/clientexercise
        [HttpPost]
        public ActionResult<ClientExercise> Post([FromBody] ClientExercise newClientExercise)
        {
            var postedClientExercise = _Repository.PostNewClientExercise(newClientExercise, _logger);
            return Ok(postedClientExercise);
        }

        // PUT api/clientexercise/5
        [HttpPut("{id}")]
        public ActionResult<ClientExercise> Put(int id, [FromBody] ClientExercise updateClientExercise)
        {
            var postedClientExercise = _Repository.UpdateClientExercise(id, updateClientExercise, _logger);
            return Ok(postedClientExercise);
        }

        /* // DELETE api/clientexercise/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
