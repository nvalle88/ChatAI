using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class ApiHeader
{
    public string AssistantConfigId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual AssistantConfig AssistantConfig { get; set; } = null!;
}
