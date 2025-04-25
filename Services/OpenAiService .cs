
using ChatAI.Models;
using ChatAI.Models.Database;
using ChatAI.Services;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OpenAiService : IOpenAiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    // private readonly AgenteAiContext _agenteAiContext;


    public OpenAiService(IHttpClientFactory httpClientFactory)//, AgenteAiContext agenteAiContext)
    {
        _httpClientFactory = httpClientFactory;

    }

    public async Task<HttpResponseMessage> CreateAssistantAsync()
    {
        var client = _httpClientFactory.CreateClient("OpenAI");

        var body = new
        {
            model = "gpt-4o",
            name = "Assistant473",
            instructions = "",
            tools = new[] { new { type = "code_interpreter" } },
            tool_resources = new
            {
                file_search = new
                {
                    vector_store_ids = new[] { "vs_UzB9RLN4URtz1L7sC3rYzNwo" }
                }
            },
            temperature = 1,
            top_p = 1
        };

        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        return await client.PostAsync("openai/assistants?api-version=2024-05-01-preview", content);
    }

    public async Task<ResponseCreateThread> CreateThreadAsync(string content, string url, List<ApiHeader> headers)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("OpenAI");


            foreach (var header in headers)
            {
                if (!client.DefaultRequestHeaders.Contains(header.Key))
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var body = new
            {
                messages = new[] { new { role = "user", content } }
            };

            var contentHttp = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, contentHttp);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseCreateThread>(responseContent);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<ResponseCreateRun> CreateRunAsync(string url, string assistantId, string additionalInstruction, List<ApiHeader> headers)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("OpenAIRUN");

            client.DefaultRequestHeaders.Clear();

            foreach (var header in headers)
            {
                if (!client.DefaultRequestHeaders.Contains(header.Key))
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var body = new
            {
                assistant_id = assistantId,
                additional_instructions = additionalInstruction
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var responese= await client.PostAsync(url, content);
            var responseContent = await responese.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseCreateRun>(responseContent);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<ResponseMessage> GetThreadMessagesAsync(string url, List<ApiHeader> headers)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("OpenAI");
            foreach (var header in headers)
            {
                if (!client.DefaultRequestHeaders.Contains(header.Key))
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            var response= await client.GetAsync($"{url}");
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ResponseMessage>(responseContent);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

   
}
