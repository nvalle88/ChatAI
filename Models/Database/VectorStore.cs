using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class VectorStore
{
    public string Identifier { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AssistantVectorStore> AssistantVectorStores { get; set; } = new List<AssistantVectorStore>();
}
