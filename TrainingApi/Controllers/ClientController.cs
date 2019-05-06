
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;



namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<Client> _logger;

        public ClientController(IRepository repository, ILogger<Client> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/client
        [HttpGet]
        public ActionResult<IEnumerable<Client>> Get()
        {
            var clients = _Repository.GetClients(_logger);
            return Ok(clients);
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Client>> GetByid(int id)
        {
            var clients = _Repository.GetClientById(id, _logger);
            return Ok(clients);
        }

        /// <summary>
        /// Currently only searches by ObjectIdentifier
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Client</returns>
        [HttpPost("query")]
        public ActionResult<Client> Query([FromBody] Client client)
        {
            if(client.ObjectIdentifier != null)
            {
                client = _Repository.GetClientByObjectIdentifier(client.ObjectIdentifier, _logger);
            }
            
            return Ok(client);
        }

        // POST api/client
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client newClient)
        {
            var postedClient = _Repository.PostNewClient(newClient, _logger);
            return Ok(postedClient);
        }

        // PUT api/client/5
        [HttpPut("{id}")]
        public ActionResult<Client> Put(int id, [FromBody] Client updateClient)
        {
            var postedClient = _Repository.UpdateClient(id, updateClient, _logger);
            return Ok(postedClient);
        }

       /* // DELETE api/client/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
