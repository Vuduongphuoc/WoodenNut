using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    public static StickManager instance;

    public List<ShapeAndHoleObjects> typeShape;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
[System.Serializable]
public class ShapeAndHoleObjects
{
    public enum TypeOfShape
    {
        Normal_Shape, X_Shape, Circle_Shape, L_Shape, C_Shape, Hexa_Shape, Square_Shape, T_Shape, Long_Shape
    }
    public TypeOfShape type;
    public int holeNumber;
    public List<GameObject> holeOnShapeContainer;
}

