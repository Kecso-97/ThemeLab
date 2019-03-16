using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEngine_home))]
public class GeneratorHelper : Editor {
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        LevelEngine_home engine = (LevelEngine_home)target;
        if(GUILayout.Button("Generate next level"))
        {
            engine.NextLevel();
        }
    }
}
