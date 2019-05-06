
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingApi.Data;


namespace TrainingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoLibraryController : ControllerBase
    {
        private readonly IRepository _Repository;
        private ILogger<VideoLibrary> _logger;

        public VideoLibraryController(IRepository repository, ILogger<VideoLibrary> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        // GET api/videolibrary
        [HttpGet]
        public ActionResult<IEnumerable<VideoLibrary>> Get()
        {
            var videolibrarys = _Repository.GetVideoLibraries(_logger);
            return Ok(videolibrarys);
        }

        // GET api/videolibrary/5
        [HttpGet("{id}")]
        public ActionResult<VideoLibrary> Get(int id)
        {
            var videolibrary = _Repository.GetVideoById(id, _logger);
            return Ok(videolibrary);
        }

        // POST api/videolibrary
        [HttpPost]
        public ActionResult<VideoLibrary> Post([FromBody] VideoLibrary newvideolibrary)
        {
            var  postedvideolibrary = _Repository.PostNewVideo(newvideolibrary, _logger);
            return Ok(postedvideolibrary);
        }

        // PUT api/videolibrary/5
        [HttpPut("{id}")]
        public ActionResult<VideoLibrary> Put(int id, [FromBody] VideoLibrary updatevideolibrary)
        {
            var  postedvideolibrary = _Repository.UpdateVideo(id, updatevideolibrary, _logger);
            return Ok(postedvideolibrary);
        }

       /* // DELETE api/videolibrary/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
