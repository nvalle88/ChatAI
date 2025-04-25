namespace ChatAI.Services
{
    using ChatAI.Models;
    using ChatAI.Models.Database;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IOpenAiService
    {
        Task<HttpResponseMessage> CreateAssistantAsync();
        Task<ResponseCreateThread> CreateThreadAsync(string content, string url, List<ApiHeader> headers);
        Task<ResponseCreateRun> CreateRunAsync(string url, string assistantId, string additionalInstruction, List<ApiHeader> headers);
        Task<ResponseMessage> GetThreadMessagesAsync(string url, List<ApiHeader> headers);
    }
}
