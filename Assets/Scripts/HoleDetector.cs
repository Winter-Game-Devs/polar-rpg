using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetector : MonoBehaviour
{
    [SerializeField] GameObject beacon;
    [SerializeField] UIManager uiManager;
    public bool hole;
    [SerializeField] Transform holeDigPosition;
    public Animator holeAnim;
    [SerializeField] SpriteRenderer holeRenderer;
    [SerializeField] Sprite holeSprite;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (beacon == null) Debug.LogError("beacon is missing");
        if (uiManager == null) Debug.LogError("uiManager is missing");
        if (holeRenderer == null) Debug.LogError("holeRenderer is missing");
        if (holeAnim == null) Debug.LogError("holeAnim is missing");
        if (holeSprite == null) Debug.LogError("holeSprite is missing");
        hole = false;
        StartCoroutine(DestroyHoleCo());
    }

    private void Update()
    {
        if(FindObjectOfType<Player>().makingHole)
        {
            holeAnim.SetBool("buildHole", true);
        }
        /*
        if (FindObjectOfType<Player>().fishing)
        {
            holeAnim.SetBool("isFishing", true);
        }
        

        if (FindObjectOfType<Player>().digPosition != null)
        {
            uiManager.dig.color = Color.red;
            uiManager.fish.color = Color.green;
            uiManager.digButton.interactable = false;
            uiManager.fishButton.interactable = false;
        }
        */
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
            collision.GetComponent<Player>().animator.SetBool("isFishing", false);
        }
    }

    public void StopDig()
    {
        //holeAnim.SetBool("buildHole", false);
        FindObjectOfType<Player>().animator.SetBool("isDigging", false);
        hole = true;
        FindObjectOfType<Player>().canMove = true;
        uiManager.fish.color = Color.green;
        uiManager.fishButton.interactable = true;
        uiManager.digButton.interactable = false;
    }

    IEnumerator DestroyHoleCo()
    {
        float waitingTime = UnityEngine.Random.Range(20f, 25f);
        yield return new WaitForSeconds(waitingTime);
        FindObjectOfType<HoleSpawner>().SpawnObjectAtRandomPosition();
        if (!FindObjectOfType<Player>().isFishing && !FindObjectOfType<Player>().isDiggingHole && !FindObjectOfType<Player>().fishing)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
