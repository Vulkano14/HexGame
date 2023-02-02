using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodepathfinging
{
    public Vector2 pos;

    public float gCost;
    public float hCost;
    public float fCost;

    public PathNodepathfinging cameFromNode;

    public int open = 0;
    public int oldOpen = 0;

    public PathNodepathfinging(Vector2 pos)
    {
        this.pos = pos;
    }

    public void CalcFcost()
    {
        fCost = gCost + hCost;
    }
}
