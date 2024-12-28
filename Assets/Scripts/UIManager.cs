using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishCountText;
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
}
