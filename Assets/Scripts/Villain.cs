using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    //Properties to control speed, shifting interval and the cloning variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionInterval;
    private Vector2 moveDirection;
    private float timer;
    [SerializeField] private int villainCounter;
    [SerializeField] private GameObject VillainPrototype;

    void Start()
    {
        timer = changeDirectionInterval;
        ChooseRandomDirection();
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
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Player collided with a villain so the villain will be destroyed
        //The destroyed villain will be replaced with 2 more to keep the game dynamic
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught the villain");
            for (int i = 0; i < villainCounter; i++)
            {
                GameObject VillainClone = Instantiate(VillainPrototype);
                VillainClone.transform.position = new Vector2(0,0);
            }
            Destroy(gameObject);
            villainCounter *= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
