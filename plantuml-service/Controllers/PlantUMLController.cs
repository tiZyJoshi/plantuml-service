﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlantUml.Net;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace plantuml_service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlantUMLController : ControllerBase
    {
        private readonly ILogger<PlantUMLController> _logger;
        private readonly IPlantUmlRenderer _renderer;

        public PlantUMLController(ILogger<PlantUMLController> logger, IPlantUmlRenderer renderer)
        {
            _logger = logger;
            _renderer = renderer;
        }

        // GET api/<PlantUMLController>/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromForm] string code)
        {
            var guid = Guid.NewGuid();
            _logger.LogInformation($"Get Request {guid}");
            if (code is null)
            {
                _logger.LogInformation($"Failed: Empty Get Request {guid}");
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            var bytes = await _renderer.RenderAsync(code, OutputFormat.Png); //"Bob -> Alice : Hello"
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(bytes);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            _logger.LogInformation($"Diagram successfully generated {guid}");
            return result;
        }
    }
}
