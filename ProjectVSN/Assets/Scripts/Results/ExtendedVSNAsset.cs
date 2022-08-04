using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedVSNAsset : VSNAsset
{
    public VSNFile[] Files_ { get; }
    public VSNUses[] Uses_ { get; } 
    public VSNAsset[] Sources_ { get; }
    public VSNAsset[] Targets_ { get; }


}
