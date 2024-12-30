using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetector : MonoBehaviour
{
    [SerializeField] GameObject beacon;
    [SerializeField] UIManager uiManager;
    [SerializeField] bool hole;
    [SerializeField] Transform holeDigPosition;
    [SerializeField] Animator holeAnim;
    [SerializeField] SpriteRenderer holeRenderer;
    [SerializeField] Sprite holeSprite;

    private void Start()
    {
        if (beacon == null) Debug.LogError("beacon is missing");
        if (uiManager == null) Debug.LogError("uiManager is missing");
        if (holeRenderer == null) Debug.LogError("holeRenderer is missing");
        if (holeAnim == null) Debug.LogError("holeAnim is missing");
        if (holeSprite == null) Debug.LogError("holeSprite is missing");
    }

    private void Update()
    {
        if(FindObjectOfType<Player>().makingHole)
        {
            holeAnim.SetBool("buildHole", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            beacon.SetActive(false);
            collision.GetComponent<Player>().digPosition = holeDigPosition;

            if (!hole)
            {
                uiManager.digButton.interactable = true;
                uiManager.dig.color = Color.green;
            }
            else
            {
                
                uiManager.dig.color = Color.red;
                uiManager.fish.color = Color.green;
                uiManager.digButton.interactable = false;
                uiManager.fishButton.interactable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            beacon.SetActive(true);
            uiManager.digButton.interactable = false;
            uiManager.fishButton.interactable = false;
            uiManager.dig.color = Color.red;
            uiManager.fish.color = Color.red;
            collision.GetComponent<Player>().digPosition = null;
            collision.GetComponent<Player>().animator.SetBool("isDigging", false);
        }
    }

    public void StopDig()
    {
        //holeAnim.SetBool("buildHole", false);
        FindObjectOfType<Player>().animator.SetBool("isDigging", false);
        hole = true;
        uiManager.fish.color = Color.green;
        uiManager.fishButton.interactable = true;
        uiManager.digButton.interactable = false;
    }
}
