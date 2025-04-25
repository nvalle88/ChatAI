namespace ChatAI.Models
{

    public class ResponseCreateThread
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created_at { get; set; }
        public Metadata metadata { get; set; }
        public Tool_Resources tool_resources { get; set; }
    }

    public class Metadata
    {
    }

    public class Tool_Resources
    {
    }




    public class ResponseCreateRun
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created_at { get; set; }
        public string assistant_id { get; set; }
        public string thread_id { get; set; }
        public string status { get; set; }
        public object started_at { get; set; }
        public int expires_at { get; set; }
        public object cancelled_at { get; set; }
        public object failed_at { get; set; }
        public object completed_at { get; set; }
        public object required_action { get; set; }
        public object last_error { get; set; }
        public string model { get; set; }
        public string instructions { get; set; }
        public Tool[] tools { get; set; }
        public Tool_Resources tool_resources { get; set; }
        public Metadata metadata { get; set; }
        public float temperature { get; set; }
        public float top_p { get; set; }
        public object max_completion_tokens { get; set; }
        public object max_prompt_tokens { get; set; }
        public Truncation_Strategy truncation_strategy { get; set; }
        public object incomplete_details { get; set; }
        public object usage { get; set; }
        public string response_format { get; set; }
        public string tool_choice { get; set; }
        public bool parallel_tool_calls { get; set; }
    }



    public class Truncation_Strategy
    {
        public string type { get; set; }
        public object last_messages { get; set; }
    }

    public class Tool
    {
        public string type { get; set; }
    }



    public class ResponseMessage
    {
        public string _object { get; set; }
        public List<Datum> data { get; set; }
        public string first_id { get; set; }
        public string last_id { get; set; }
        public bool has_more { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created_at { get; set; }
        public string assistant_id { get; set; }
        public string thread_id { get; set; }
        public string run_id { get; set; }
        public string role { get; set; }
        public List<Content> content { get; set; }
        public object[] attachments { get; set; }
        public Metadata metadata { get; set; }
    }

    

    public class Content
    {
        public string type { get; set; }
        public Text text { get; set; }
    }

    public class Text
    {
        public string value { get; set; }
        public Annotation[] annotations { get; set; }
    }

    public class Annotation
    {
        public string type { get; set; }
        public string text { get; set; }
        public int start_index { get; set; }
        public int end_index { get; set; }
        public File_Citation file_citation { get; set; }
    }

    public class File_Citation
    {
        public string file_id { get; set; }
    }


}
