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

        public PlantUMLController(ILogger<PlantUMLController> logger)
        {
            _logger = logger;
        }

        // GET api/<PlantUMLController>/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromForm] string code)
        {
            if (code is null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            var settings = new PlantUmlSettings
            {
                RenderingMode = RenderingMode.Remote,
                RemoteUrl = "http://plantuml-server:8080/"
            };
            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(settings);
            var bytes = await renderer.RenderAsync(code, OutputFormat.Png); //"Bob -> Alice : Hello"
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(bytes);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            _logger.LogInformation("");
            return result;
        }
    }
}
