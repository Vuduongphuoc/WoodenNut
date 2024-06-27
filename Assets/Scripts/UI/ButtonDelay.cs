using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDelay : MonoBehaviour
{
    public void Delay(Button a)
    {
        StartCoroutine(DelayActive(a));
    }
    // Update is called once per frame
    private IEnumerator DelayActive(Button a)
    {
        
        a.interactable = false;
        yield return new WaitForSeconds(1f);
        a.interactable = true;

    }
}
