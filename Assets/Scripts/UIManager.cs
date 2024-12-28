using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishCountText;
    public GameObject inventoryPanel;
    public GameObject statsPanel;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnNewFishCount += UpdateFishCountUI;
        if (fishCountText == null) Debug.LogError("FishCountText is missing");
    }

    private void UpdateFishCountUI(int fishCount)
    {
        if (fishCountText)
            fishCountText.SetText(fishCount.ToString());
    }

    public void ToggleInventoryPanel()
    {
        statsPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void ToggleStatsPanel()
    {
        inventoryPanel.SetActive(false);
        statsPanel.SetActive(true);
    }
}
