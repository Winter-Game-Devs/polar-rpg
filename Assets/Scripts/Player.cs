using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.8f;
    private Vector2 movement;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (rigidBody == null) Debug.LogError("Rigidbody2D component is missing");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer component is missing");
        if (animator == null) Debug.LogError("Animator component is missing");
    }

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Update animator variable IsMoving
        animator.SetBool("IsMoving", movement != Vector2.zero);
        // Flip the sprite if moving left
        if (movement.x != 0) spriteRenderer.flipX = movement.x > 0;
    }

    void FixedUpdate()
    {
        // Move the player
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
