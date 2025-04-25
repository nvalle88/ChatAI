using System;
using System.Collections.Generic;

namespace ChatAI.Models.Database;

public partial class Type
{
    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ApiEndpoint> ApiEndpoints { get; set; } = new List<ApiEndpoint>();
}
