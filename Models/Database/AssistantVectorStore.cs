using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class AssistantVectorStore
{
    public string AssistantConfigId { get; set; } = null!;

    public string VectorStoreId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual AssistantConfig AssistantConfig { get; set; } = null!;

    public virtual VectorStore VectorStore { get; set; } = null!;
}
