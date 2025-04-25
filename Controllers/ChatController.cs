using Microsoft.AspNetCore.Mvc;

namespace ChatAI.Controllers
{
    using Azure.Core;
    using ChatAI.Models;
    using ChatAI.Models.Database;
    using ChatAI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    [Route("[controller]")]
    public class ChatController : Controller
    {
        private const string AZURE_URL = "";
        private const string AZURE_TOKEN = "";
        private readonly IOpenAiService _iOpenAiService;
        private readonly AgenteAiContext _agenteAiContext;



        public ChatController(IOpenAiService openAiService, AgenteAiContext agenteAiContext)
        {
            _iOpenAiService = openAiService;
            _agenteAiContext = agenteAiContext ?? throw new ArgumentNullException(nameof(agenteAiContext));

        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet("api/assistant/GetAssistants")]
        public async Task<IActionResult> GetAssistant()
        {
            var assistants = await _agenteAiContext.AssistantConfigs.ToListAsync();
            return Json(assistants);

        }

        public class ChatAI
        {
            public List<string> chat_history { get; set; } = new List<string>(); // Lista vacía por defecto
            public string query { get; set; }
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            try
            {
                // Validar el modelo de entrada
                if (request == null || string.IsNullOrEmpty(request.version))
                {
                    return Json(new { success = false, error = "Invalid request" });
                }
                var assist = _agenteAiContext.AssistantConfigs.Include(x => x.ApiHeaders).Include(x => x.ApiEndpoints)
                    .Where(x => x.AssistantId == request.version)
                    .FirstOrDefault();

                var headers = assist.ApiHeaders.ToList();

                var endPoint = assist.ApiEndpoints.Where(x => x.TypeName.Equals("CreateThread")).FirstOrDefault();
                var url = $"{endPoint.PathTemplate}{endPoint.ApiVersion}";

                var thread = await _iOpenAiService.CreateThreadAsync(request.Prompt, url, headers);

                endPoint = assist.ApiEndpoints.Where(x => x.TypeName.Equals("CreateRun")).FirstOrDefault();
                url = $"{endPoint.PathTemplate.Replace("{thread_}", thread.id)}{endPoint.ApiVersion}";

                var run = await _iOpenAiService.CreateRunAsync(url, assist.AssistantId, endPoint.AdditionalInstructions ?? string.Empty, headers);

                endPoint = assist.ApiEndpoints.Where(x => x.TypeName.Equals("GetThreadMessages")).FirstOrDefault();
                url = $"{endPoint.PathTemplate.Replace("{thread_}", thread.id)}{endPoint.ApiVersion}";

                ResponseMessage responseMessagge = null;
                Datum message = null;

                int maxRetries = 30; 
                int attempts = 0;
                do
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                        responseMessagge = await _iOpenAiService.GetThreadMessagesAsync(url, headers);
                        message = responseMessagge.data.FirstOrDefault(x => x.run_id == run.id);
                        attempts++;
                    }
                    catch (Exception)
                    {
                        attempts++;
                    }

                } while (message?.content==null || message.content.Count==0 );

                if (message.content.Count >= 0)
                {
                    // Obtener los mensajes cuando el run esté completado
                    return Json(new { success = true, response = message.content?.FirstOrDefault()?.text.value});

                }


                return Json(new { success = true, response = "Run status is {runResponse.Status}, unable to fetch messages." });
                return null;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        private string GetDeploymentModel(string version)
        {
            return version switch
            {
                "v1" => "laura-ai-gmdkd-1",  // Despliegue para Versión 1
                "v2" => "laura-ai-gmdkd-2",  // Despliegue para Versión 2 (ajusta el nombre según tu configuración)
                "v3" => "laura-ai-gmdkd-3",  // Despliegue para Versión 3 (ajusta el nombre según tu configuración)
                _ => "laura-ai-gmdkd-1"      // Valor por defecto si la versión no coincide
            };
        }
    }
}
