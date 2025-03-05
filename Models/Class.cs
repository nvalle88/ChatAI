namespace ChatAI.Models
{

    public class Document
    {
        public string chunk_id { get; set; }
        public string content { get; set; }
        public string filepath { get; set; }
        public object metadata { get; set; }
        public double score { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }

    public class ChatResponse
    {
        public List<Document> documents { get; set; }
        public string reply { get; set; }
    }

    public class ChatRequest
    {
        public string Prompt { get; set; }
        public string version { get; set; }
        public List<string> ChatHistory { get; set; }
    }
}
