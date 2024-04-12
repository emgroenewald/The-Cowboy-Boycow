using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5.0f;
    public Transform player;
    public float chaseSpeed = 3.0f;
    public float attackRange = 1.0f;
    public float jumpForce = 300f;
    private Rigidbody2D rb;
    private bool isGrounded = true;


    private void Start()
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

    }

    void AttackPlayer()
    {
        Debug.Log("Attacking player!");
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);


        if (Mathf.Abs(player.position.y - transform.position.y) > 0.5f && isGrounded)
        {
            JumpTowardsPlayer();
        }
    }

    void JumpTowardsPlayer()
    {
        rb.AddForce(new Vector2(0, jumpForce));
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")  
        {
            isGrounded = true;
        }
    }
}
