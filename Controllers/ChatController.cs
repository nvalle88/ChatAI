using Microsoft.AspNetCore.Mvc;

namespace ChatAI.Controllers
{
    using ChatAI.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    [Route("[controller]")]
    public class ChatController : Controller
    {
        private const string AZURE_URL = "";
        private const string AZURE_TOKEN = "";
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
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
                var client = _httpClientFactory.CreateClient();
                var resuqetAi = new ChatAI
                {
                    chat_history = new List<string>(),
                    query = request.Prompt,
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(resuqetAi),
                    Encoding.UTF8,
                    "application/json"
                );

                string deploymentModel = GetDeploymentModel(request.version);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AZURE_TOKEN}");
                client.DefaultRequestHeaders.Add("azureml-model-deployment", deploymentModel);

                var response = await client.PostAsync(AZURE_URL, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();
                var chatResponse = JsonSerializer.Deserialize<ChatResponse>(responseData);

                // Solo retornamos el 'reply' para la vista
                return Json(new { success = true, response = chatResponse.reply });
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
