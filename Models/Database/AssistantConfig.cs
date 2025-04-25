using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class AssistantConfig
{
    public string AssistantId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ProviderId { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? Instructions { get; set; }

    public decimal Temperature { get; set; }

    public decimal TopP { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApiEndpoint> ApiEndpoints { get; set; } = new List<ApiEndpoint>();

    public virtual ICollection<ApiHeader> ApiHeaders { get; set; } = new List<ApiHeader>();

    public virtual ICollection<AssistantTool> AssistantTools { get; set; } = new List<AssistantTool>();

    public virtual ICollection<AssistantVectorStore> AssistantVectorStores { get; set; } = new List<AssistantVectorStore>();

    public virtual ApiProvider Provider { get; set; } = null!;
}
