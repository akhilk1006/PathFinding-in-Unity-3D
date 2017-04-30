using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vectorwrapper
{
    public Vector3 vector;
    public float gcost;
    public float hcost;

    public vectorwrapper(Vector3 samp)
    {
        vector = samp;
    }
    public float TotalCost
    {
        get
        {
            return gcost + hcost;
        }
    }
}
