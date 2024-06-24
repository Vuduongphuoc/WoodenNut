using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ReverseController : MonoBehaviour
{
    public static ReverseController Instance;
    public List<SpawnNut> sticks = new List<SpawnNut>();
    public List<DragDrop> screws = new List<DragDrop>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void SaveObjectsMove()
    {
        SaveStickMoveBeforeChange();
        //count++;
    }
    private void SaveStickMoveBeforeChange()
    {
        foreach(var i in sticks)
        {
            i.SavePosition();
        }
        foreach(var a in screws)
        {
            a.SavePosition();
        }
    }

    public void FindSpawnNut()
    {
        screws = GameObject.FindObjectsOfType<DragDrop>().ToList();
        sticks = GameObject.FindObjectsOfType<SpawnNut>().ToList();
        DragDropController.instance.ScrewAndHoles();
    }
    public void ClearSpawnNut()
    {
        screws = null;
        //screws.Free();
    }
    public void ActiveReverse()
    {
        foreach(var i in sticks)
        {
            i.GetComponentInParent<Rigidbody2D>().isKinematic = true;
            i.gameObject.SetActive(true);
            if (i.PosReverse.Count > 0 && i.RotReverse.Count > 0)
            {
                i.transform.position = i.PosReverse.Last();
                i.PosReverse.Remove(i.PosReverse.Last());
                i.transform.rotation = i.RotReverse.Last();
                i.RotReverse.Remove(i.RotReverse.Last());
                if (!i.enabled)
                {
                    i.enabled = true;
                }
                UIManager.instance.n = "undo";
            }
            else
            {
                print("Sticks already at the start position!");
            }
            i.GetComponentInParent<Rigidbody2D>().isKinematic = false;
        }
        Screw parent;
        foreach( var a in screws)
        {
            parent = a.GetComponentInParent<Screw>();
            parent.GetComponent<Rigidbody2D>().isKinematic = true;
            if(a.screwPos.Count > 0)
            {
                parent.gameObject.transform.position = a.screwPos.Last();
                
                a.screwPos.Remove(a.screwPos.Last());
            }
            else
            {
                print("Screw already at the start position!");
            }
            parent.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}




