using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeveLManager : MonoBehaviour
{
    public static LeveLManager Instance;
    public List<LeveLScriptableObject> levels = new List<LeveLScriptableObject>();
    public int indexCurrentLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Reset()
    {
        levels.Clear();
        foreach(LeveLScriptableObject data in Resources.LoadAll<LeveLScriptableObject>("Levels"))
        {
            levels.Add(data);
        }
        levels.OrderBy(x => x.name);
    }
}
