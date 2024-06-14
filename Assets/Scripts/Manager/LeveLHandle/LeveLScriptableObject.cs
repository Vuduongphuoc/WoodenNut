using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeveLScriptableObject : ScriptableObject
{
    public int levelIndex;
    public bool isFinish;
    public List<LevelObjData> ObjPrefabs;
}
[Serializable]
public class LevelObjData
{
    public int ObjID;
    public int screwSprID;
    public TypeOfColor woodColorID;
    public int woodShapeID;
    public Vector2 ObjPos;
    public Quaternion ObjRotate;
    public Vector3 ObjScale;
}
