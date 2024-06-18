using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;
    public List<GameObject> objsToSwap;

    public void AddTabBtns(TabButton btn)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(btn);
    }

    public void OnTabEnter(TabButton btn)
    {
        ResetTabs();
        if (selectedTab == null || btn != selectedTab)
        {
            btn.backGround.sprite = btn.btnStatus[0];
        }
    }
    public void OnTabExit(TabButton btn)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton btn)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }
        selectedTab = btn;
        selectedTab.Select();
        ResetTabs();
        btn.backGround.sprite = btn.btnStatus[0];
        int index = btn.transform.GetSiblingIndex();
        for(int i =0; i< objsToSwap.Count; i++)
        {
            if(i == index)
            {
                objsToSwap[i].SetActive(true);
            }
            else
            {
                objsToSwap[i].SetActive(false);
            }
        }
        
    }
    public void ResetTabs()
    {
        foreach(TabButton btn in tabButtons)
        {
            if (selectedTab != null && btn == selectedTab)
            {
                continue;
            }
            btn.backGround.sprite = btn.btnStatus[1];
        }
    }

}
