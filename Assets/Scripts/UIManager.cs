using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI fishCountText, totalCountText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameStartCountdownText;
    public TextMeshProUGUI moveSpeedOnSnowText, moveSpeedOnIceText;
    public GameObject inventoryPanel;
    public GameObject countdownUI;
    public GameObject gameOverUI;
    public GameObject statsPanel;
    public Player player;

    public Igloo igloo;

    [Header("Fishing Tools")]
    public Image dig;
    public Image fish;
    public Button digButton, fishButton;

    // Start is called before the first frame update
    void Start()
    {   
        totalCountText.SetText(igloo.inventory.ToString());
        Player.OnNewFishCount += UpdateFishCountUI;
        Player.OnNewDeliveryCount += UpdateTotalCountUI;
        if (fishCountText == null) Debug.LogError("FishCountText is missing");
        if (levelText == null) Debug.LogError("LevelText is missing");
        if (moveSpeedOnSnowText == null) Debug.LogError("MoveSpeedOnSnowText is missing");
        if (moveSpeedOnIceText == null) Debug.LogError("MoveSpeedOnIceText is missing");
        if (dig == null) Debug.LogError("dig is missing");
        if (fish == null) Debug.LogError("fish is missing");
        if (fishButton == null) Debug.LogError("fishButton is missing");
        if (digButton == null) Debug.LogError("DigButton is missing");
        FindObjectOfType<HoleSpawner>().SpawnObjectAtRandomPosition();

        PlutoGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        HideCountdownUI();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
       if (PlutoGameManager.Instance.IsCountdownToStart()) ShowCountdownUI();
       else HideCountdownUI();
       if (PlutoGameManager.Instance.IsGameOver()) ShowGameOverUI();
       
    }

    private void ShowCountdownUI() {
        countdownUI.SetActive(true);
    }

    private void ShowGameOverUI() {
        gameOverUI.SetActive(true);
    }

    private void HideGameOverUI() {
        gameOverUI.SetActive(true);
    }

    private void HideCountdownUI() {
        countdownUI.SetActive(false);
    }


    private void UpdateFishCountUI(int fishCount)
    {
        if (fishCountText)
            fishCountText.SetText(fishCount.ToString());
    }

    private void UpdateTotalCountUI(int fishDelivered)
    {
        String totalText = (igloo.inventory + fishDelivered).ToString();
        if (totalCountText)
            totalCountText.SetText(totalText);
    }

    public void Update()
    {
        totalCountText.SetText(igloo.inventory.ToString());
        UpdateLevelUI();
        UpdateSpeedUI();
        gameStartCountdownText.text = Mathf.Ceil(PlutoGameManager.Instance.GetCountdownToStartTimer()).ToString();
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
    
    public void DigTheHole()
    {
        dig.color = Color.red;
        //fish.color = Color.green;
        player.canMove = false;
        player.isDiggingHole = true;
        digButton.interactable = false;
    }

    public void Fishing()
    {
        fish.color = Color.red;
        //fish.color = Color.green;
        player.canMove = false;
        player.isFishing = true;
        fishButton.interactable = false;
        StartCoroutine(SpawnFishCo());
    }

    IEnumerator SpawnFishCo()
    {
        float waitingTime = UnityEngine.Random.Range(2f, 3f);
        yield return new WaitForSeconds(waitingTime);
        if (player.isFishing)
        {
            player.SpawnAFish();
        }
        player.isFishing = false;
        player.canMove = true;
        player.animator.SetBool("isFishing", false);
        if (player.digPosition != null)
        {
            fishButton.interactable = true;
            fish.color = Color.green;
        }
    }
}
