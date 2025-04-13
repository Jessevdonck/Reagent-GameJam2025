using System;
using System.Collections.Generic;
using TilemapLogic;
using UnityEngine;

public class WireConnectionPoint : MonoBehaviour
{
    private Wire wire = null;
    private WireConnectionPoint wcp;

    private void setWireConnectionPoint(WireConnectionPoint wcp)
    {
        this.wcp = wcp;
    }
    
    private void OnMouseDown()
    {
        if (wire != null) return;
        if (PlacingSystem.instance.getIsWirePlacing())
        {
            PlacingSystem.instance.finalizeWireDrawing(this.transform.position);
        }
        else
        {
            PlacingSystem.instance.startWireDrawing(this.transform.position);
        }
    }
}
