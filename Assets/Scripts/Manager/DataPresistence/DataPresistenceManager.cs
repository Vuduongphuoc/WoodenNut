using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPresistenceManager : MonoBehaviour
{
    private GameData gameData;

    private List<IDataPresistence> dataObjs;

    public static DataPresistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Data Presistence Manager in the scene");
        }
        Instance = this;
    }
    public void Start()
    {
        this.dataObjs = FindAllDataObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //TODO - Load any saved data from a file using the data handler

        //if no data can be loaded, initialize to a new game.

        if (this.gameData != null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPresistence dataObjs in dataObjs)
        {
            dataObjs.LoadData(gameData);
        }

        Debug.Log("Load level ID = " + gameData.levelID);
    }
    public void SaveGame()
    {
        foreach (IDataPresistence dataObjs in dataObjs)
        {
            dataObjs.SaveData(ref gameData);

        }
        Debug.Log("Save level ID = " + gameData.levelID);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPresistence> FindAllDataObjects()
    {
        IEnumerable<IDataPresistence> dataObjs = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataObjs);
    }
}
