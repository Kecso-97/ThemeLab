using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLevelPartModel {

    public abstract bool FindPath(Vector2Int enterDir);

    public GameObject instance;
}
