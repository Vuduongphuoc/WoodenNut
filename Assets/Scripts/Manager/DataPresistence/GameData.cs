using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelID;
    public int objID;
    public Vector3 objPosition;
    public Quaternion objRotation;

    public GameData()
    {
        this.levelID = 0;
        objID = 0;
        objPosition = Vector3.zero;
        objRotation = Quaternion.identity;
        
    }
}
