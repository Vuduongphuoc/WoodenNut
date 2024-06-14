using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeveLObject : MonoBehaviour
{
    public abstract int GetObjectID();

    #region Objects Data
    public virtual void SetData(LevelObjData objData)
    {

    }
    public virtual LevelObjData GetData()
    {
        LevelObjData lvlObj = new LevelObjData();
        lvlObj.ObjID = GetObjectID();
        if(lvlObj.ObjID >5 && lvlObj.ObjID < 20)
        {
            lvlObj.woodColorID = GetComponent<WoodStick>().color;
        }
        lvlObj.ObjPos = transform.position;
        lvlObj.ObjRotate = transform.rotation;
        lvlObj.ObjScale = transform.localScale;
        return lvlObj;
    }
    #endregion
}
