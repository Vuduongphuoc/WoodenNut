using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WoodBoard : MonoBehaviour
{
    public static WoodBoard Instance;
    public List<WoodStick> sticks = new List<WoodStick>();

    private void OnEnable()
    {
        StartCoroutine(FindStick());
    }
    private IEnumerator FindStick()
    {
        yield return new WaitForSeconds(1f);
        sticks = GameObject.FindObjectsOfType<WoodStick>().ToList();
        foreach (WoodStick stick in sticks)
        {
            stick.SetBoard(this);
        }
    }
    private void CheckForSticks()
    {
        if (sticks.Count == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.FinishGamePlaytState);
        }
    }
    public void RemoveStick(WoodStick a)
    {
        if (sticks.Contains(a))
        {
            sticks.Remove(a);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[7]);
            CheckForSticks();
        }
    }

}
