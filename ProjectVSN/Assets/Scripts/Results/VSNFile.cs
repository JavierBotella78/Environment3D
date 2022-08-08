using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSNFile
{
    public string Id_ { get; }
    public string Title_ { get; }
    public VSNUses[] Uses_ { get; set; }

    public VSNFile(string id, string title)
    {
        Id_ = id;
        Title_ = title;
    }
}
