using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class ApiProvider
{
    public string ProviderId { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string BaseUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AssistantConfig> AssistantConfigs { get; set; } = new List<AssistantConfig>();
}
