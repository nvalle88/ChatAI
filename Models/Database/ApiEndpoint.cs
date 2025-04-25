using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class ApiEndpoint
{
    public string AssistantConfigId { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string PathTemplate { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string ApiVersion { get; set; } = null!;

    public string? AdditionalInstructions { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AssistantConfig AssistantConfig { get; set; } = null!;

    public virtual Type TypeNameNavigation { get; set; } = null!;
}
