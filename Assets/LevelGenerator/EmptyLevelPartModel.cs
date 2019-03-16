using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyLevelPartModel : AbstractLevelPartModel
{
    public override bool FindPath(Vector2Int enterDir)
    {
        //Debug.Log("EmptyPart has returned with FALSE");
        return false;
    }
}
