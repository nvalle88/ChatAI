using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class Tool
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<AssistantTool> AssistantTools { get; set; } = new List<AssistantTool>();
}
