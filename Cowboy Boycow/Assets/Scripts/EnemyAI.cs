using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 3.0f;
    public float detectionRange = 5.0f;
    public float attackRange = 1.0f;
    public float jumpForce = 300f;
    public int health = 100;
    public float knockbackForce = 5f;  // Adjust this value as needed
    private Rigidbody2D rb;
    private bool isGrounded = true;
    [SerializeField] float timeToCheck = 0.5f;  // Time interval to check if stuck
    private Vector2 lastPosition;
    private float checkTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            ChasePlayer();
        }

        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }

      
        checkTimer += Time.deltaTime;
        if (checkTimer >= timeToCheck)
        {
            if (Vector2.Distance(transform.position, lastPosition) < 0.1f) 
            {
                if (isGrounded)
                {
                    JumpTowardsPlayer();
                }
            }
            lastPosition = transform.position;
            checkTimer = 0;
        }
    }


    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }


    void JumpTowardsPlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;  
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }


    void AttackPlayer()
    {
        Debug.Log("Attacking player!");
        if (player.TryGetComponent(out Rigidbody2D playerRb))
        {
            Vector2 knockbackDir = (player.position - transform.position).normalized;
            playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }
    }

   
}
