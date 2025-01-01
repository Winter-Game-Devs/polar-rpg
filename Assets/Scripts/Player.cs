using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Event when the bear caught a fish
    public static event Action<int> OnFishCatched;
    // Event that tells the current number of fish caught
    public static event Action<int> OnNewFishCount;

    public float moveSpeedOnIce = 0.3f;
    public float moveSpeedOnSnow = 1.8f;
    public float moveSpeed = 1.8f;
    public float slideFriction = 0.4f; // How quickly the player slows down when sliding (0.95 = very slippery)
    public float inputInfluence = 1f; // How much input affects movement on ice
    private Vector2 movement;
    private Vector2 velocity; // Tracks momentum during sliding

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool slide = false, canMove = true;

    public PlayerStats playerStats;

    public int fishCount = 0;

    [Header("Fishing Tools")]
    [SerializeField] Transform castPoint;
    [SerializeField] float fishDistance, digMovePositionSpeed; // this is speed to move to hole position
    public bool fishDetector, isFacingLeft, isDiggingHole, isFishing; //i build a hole detector, we might not needed. But is here for now. I made something much simple.
    public Transform digPosition, spawnFishPosition;
    public GameObject hole;
    public bool makingHole, fishing;
    public GameObject theFish;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (playerStats == null) Debug.LogError("PlayerStats scriptable object is missing");
        if (rigidBody == null) Debug.LogError("Rigidbody2D component is missing");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer component is missing");
        if (animator == null) Debug.LogError("Animator component is missing");
        if (spawnFishPosition == null) Debug.LogError("spawnFishPosition is missing");
        if (theFish == null) Debug.LogError("theFish is missing");
    }

    void Update()
    {
        if (canMove)
        {
            // Get input
            if (!slide)
            {
                moveSpeed = moveSpeedOnSnow;
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                movement = movement.normalized;
                velocity = movement * moveSpeed; // Direct movement when not sliding
                if (spriteRenderer.flipX)
                {
                    isFacingLeft = false;
                }
                else
                {
                    isFacingLeft = true;
                }
            }
            else
            {
                moveSpeed = moveSpeedOnIce;
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");
                movement = movement.normalized; // Ensure consistent input direction
                velocity += movement * moveSpeed * inputInfluence;

                // Clamp velocity to a maximum speed
                velocity = Vector2.ClampMagnitude(velocity, moveSpeedOnIce * 2f);

                if (spriteRenderer.flipX)
                {
                    isFacingLeft = false;
                }
                else
                {
                    isFacingLeft = true;
                }
            }
            // Update animator variable IsMoving
            animator.SetBool("IsMoving", movement != Vector2.zero);
            // Flip the sprite if moving left
            if (movement.x != 0) spriteRenderer.flipX = movement.x > 0;

            if (FishDetector(fishDistance))
            {
                Debug.Log("you find a hole");
                //isFacingLeft = true;
            }
            else
            {
                Debug.Log("you find nothing");
                //isFacingLeft = false;
            }
        }

        if (isDiggingHole)
        {
            if (transform.position.x < digPosition.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, digPosition.position, digMovePositionSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
            if (Vector3.Distance(transform.position, digPosition.position) < 0.01f)
            {
                spriteRenderer.flipX = false;
                isDiggingHole = false; // Switch direction
                makingHole = true;
                animator.SetBool("IsMoving", false);
                animator.SetBool("isDigging", true);
            }
        }

        if (isFishing)
        {
            if (transform.position.x < digPosition.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, digPosition.position, digMovePositionSpeed * Time.deltaTime);
            animator.SetBool("IsMoving", true);
            if (Vector3.Distance(transform.position, digPosition.position) < 0.01f)
            {
                spriteRenderer.flipX = false;
                isDiggingHole = false; // Switch direction
                fishing = true;
                animator.SetBool("IsMoving", false);
                animator.SetBool("isFishing", true);
            }
        }
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

    public void FishCatched(int numberOfFishCatched = 1)
    {
        fishCount += numberOfFishCatched;
        OnFishCatched?.Invoke(numberOfFishCatched);
        OnNewFishCount?.Invoke(fishCount);
    }

    public float GetLevel()
    {
        return playerStats.level.GetValue();
    }

    public float GetSpeed()
    {
        return playerStats.speed.GetValue() * moveSpeed;
    }
    
    bool FishDetector(float distance)
    {        
        var castDistance = distance;
        if (isFacingLeft)
        {
            castDistance = -distance;
        }
        Vector2 endPos = castPoint.position + Vector3.right * castDistance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Fishing"));

        if(hit.collider !=  null)
        {
            if(hit.collider.gameObject.CompareTag("Hole"))
            {
                fishDetector = true;
            }
            else
            {
                fishDetector = false;
            }

            Debug.DrawLine(castPoint.position, endPos, Color.red);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.green);
        }

        return fishDetector;
    }

    public void StopFishingOrDigging()
    {
        canMove = true;
        fishing = false;
    }

    public void SpawnAFish()
    {
        float randomX = UnityEngine.Random.Range(-0.5f, -1f);
        float randomY = UnityEngine.Random.Range(-1f, 1f);

        Vector3 spawnPosition = new Vector3(spawnFishPosition.position.x + randomX, spawnFishPosition.position.y + randomY, spawnFishPosition.position.z);

        // Instantiate the GameObject
        Instantiate(theFish, spawnPosition, Quaternion.identity);
        //StopFishing();
    }
    
}
