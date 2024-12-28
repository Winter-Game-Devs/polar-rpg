using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishCountText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moveSpeedOnSnowText, moveSpeedOnIceText;
    public GameObject inventoryPanel;
    public GameObject statsPanel;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnNewFishCount += UpdateFishCountUI;
        if (fishCountText == null) Debug.LogError("FishCountText is missing");
        if (levelText == null) Debug.LogError("LevelText is missing");
        if (moveSpeedOnSnowText == null) Debug.LogError("MoveSpeedOnSnowText is missing");
        if (moveSpeedOnIceText == null) Debug.LogError("MoveSpeedOnIceText is missing");
    }

    private void UpdateFishCountUI(int fishCount)
    {
        if (fishCountText)
            fishCountText.SetText(fishCount.ToString());
    }

    public void Update()
    {
        UpdateLevelUI();
        UpdateSpeedUI();
    }

    public void UpdateLevelUI()
    {
        levelText.SetText(player.GetLevel().ToString());
    }

    public void UpdateSpeedUI()
    {
        moveSpeedOnSnowText.SetText(player.moveSpeedOnSnow.ToString());
        moveSpeedOnIceText.SetText(player.moveSpeedOnIce.ToString());
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
