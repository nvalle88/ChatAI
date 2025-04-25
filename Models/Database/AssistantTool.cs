using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class AssistantTool
{
    public string AssistantConfigId { get; set; } = null!;

    public string ToolCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual AssistantConfig AssistantConfig { get; set; } = null!;

    public virtual Tool ToolCodeNavigation { get; set; } = null!;
}
