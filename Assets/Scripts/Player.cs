using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1.8f;
    public float slideFriction = 0.4f; // How quickly the player slows down when sliding (0.95 = very slippery)
    public float inputInfluence = 1f; // How much input affects movement on ice
    private Vector2 movement;
    private Vector2 velocity; // Tracks momentum during sliding

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool slide = false;

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
        if (!slide)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;
            velocity = movement * moveSpeed; // Direct movement when not sliding
        }
        else
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical"); 
            velocity += movement * moveSpeed * inputInfluence;
        }
        // Update animator variable IsMoving
        animator.SetBool("IsMoving", movement != Vector2.zero);
        // Flip the sprite if moving left
        if (movement.x != 0) spriteRenderer.flipX = movement.x > 0;
    }

    void FixedUpdate()
    {
        if (slide)
        {
            // Apply sliding friction
            velocity *= slideFriction;

            // Stop movement if velocity is very small (prevents infinite slow drift)
            if (velocity.magnitude < 0.01f) velocity = Vector2.zero;
        }

        // Move the player
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
    }
}
