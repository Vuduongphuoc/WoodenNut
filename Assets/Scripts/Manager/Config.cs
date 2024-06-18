using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config Instance;
    [Header("Factory Config")]
    [SerializeField] private LeveLObjectFactory objFactory;
    

    public LeveLObjectFactory gameObjectFactory { get => objFactory; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        objFactory = Resources.Load("LeveL Object", typeof(ScriptableObject)) as LeveLObjectFactory;
    }
}
