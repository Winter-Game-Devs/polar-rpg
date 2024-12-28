using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionInterval;
    private Vector2 moveDirection;
    private float timer;

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

    void ChooseRandomDirection() {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        moveDirection = new Vector2(randX, randY).normalized;
    }

    void MoveCharacter() {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
