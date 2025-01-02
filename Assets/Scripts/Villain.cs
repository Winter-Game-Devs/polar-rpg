using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Villain : MonoBehaviour
{
    //Properties to control speed, shifting interval and the cloning variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionInterval;
    private Vector2 moveDirection;
    private float timer;

    [SerializeField] private GameObject targetIgloo; 
    private Igloo igloo;
    private bool canMove = true;
    public bool isThiefing = false;
    [SerializeField] private int villainCounter;
    [SerializeField] private GameObject VillainPrototype;
    void Start()
    {
        timer = changeDirectionInterval;
        ChooseRandomDirection();

        if (targetIgloo) {
            igloo = targetIgloo.GetComponent<Igloo>();
            
        }

    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer<=0) {
            ChooseRandomDirection();
            timer = changeDirectionInterval;
        }

        MoveCharacter();
    }

    //Keep the Villain randomly moving in direction
    void ChooseRandomDirection() {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        moveDirection = new Vector2(randX, randY).normalized;
    }

    void MoveCharacter() {
        if (canMove) {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //Player collided with a villain so the villain will be destroyed
        //The destroyed villain will be replaced with 2 more to keep the game dynamic
        if (other.CompareTag("Player"))
        {   
            transform.localScale += new Vector3(.5f, 0.5f, .5f);
            Debug.Log("Player caught the villain");
            
            for (int i = 0; i < villainCounter; i++)
            {
                GameObject VillainClone = Instantiate(VillainPrototype);
                VillainClone.transform.position = new Vector2(Random.Range(-10, 10),Random.Range(-10, 10));
            }
            Destroy(gameObject);
            villainCounter *= 2;
        }

        if (other.CompareTag("Igloo"))
        {
            Debug.Log("Villain caught the house, it has");
            StartCoroutine(DecreaseInventoryOverTime());
            transform.localScale += new Vector3(.8f, 0.8f, .8f);
            canMove = false;
        }
    }

    private IEnumerator DecreaseInventoryOverTime()
    {
        isThiefing = true;
        // Keep decreasing the inventory every second until it reaches 0
        while (igloo.Inventory > 0)
        {
            igloo.SetInventory(igloo.Inventory - 1);
            Debug.Log("Inventory decreased: " + igloo.Inventory);
            yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
        }

        // After inventory reaches 0, stop decreasing

        Debug.Log("Inventory has reached 0.");
        if (igloo.Inventory == 0) EndGame();
    }

    public void EndGame() {
        Debug.Log("Game Over");
    }

    public bool GetIsThiefing() {
        return isThiefing;
    }
}
